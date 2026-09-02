using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.Identity.Client;
using StaskoTravel.Core.IService;
using StaskoTravel.DataAccess.Repository;
using StaskoTravel.Models.Entities;
using StaskoTravel.ViewModels.Activity;
using StaskoTravel.ViewModels.Trip;
using StaskoTravel.ViewModels.TripActivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoTravel.Core.Service
{
    public class TripService : ITripService
    {
        private readonly IRepository<Trip> tripRepo;
        private readonly IRepository<Activity> activityRepo;
        private readonly IRepository<TripActivity> tripActivityRepo;

        public TripService(IRepository<Trip> _tripRepo,
                           IRepository<TripActivity> _tripActivityRepo,
                           IRepository<Activity> _activityRepo)
        {
            this.tripRepo = _tripRepo;
            this.tripActivityRepo = _tripActivityRepo;
            this.activityRepo = _activityRepo;
        }

        public async Task AddActivityToTripAsync(Guid tripId, Guid activityId)
        {
            var trip = await tripRepo.GetByIdAsync(tripId);
            var activity = await activityRepo.GetByIdAsync(activityId);

            if (trip == null)
            {
                throw new NullReferenceException("There is no trip with this ID!");
            }

            if (activity == null)
            {
                throw new NullReferenceException("There is no activity with this ID!");
            }

            var tripActivity = new TripActivity
            {
                Trip = trip,
                Activity = activity,
                ScheduledDate = DateOnly.FromDateTime(DateTime.Now),
                EstimatedCost = 10,
            };

            await tripActivityRepo.AddAsync(tripActivity);
            await tripActivityRepo.SaveChangesAsync();
        }

        public async Task AddSepcificationsAsync(TripActivityCreateViewModel vm)
        {
            var tripActivity = await tripActivityRepo.GetAllAttached()
                .FirstOrDefaultAsync(ta => ta.TripId == vm.TripId && ta.ActivityId == vm.ActivityId);

            if (tripActivity == null)
            {
                throw new NullReferenceException("There is no TripActivity with these IDs!");
            }

            tripActivity.ScheduledDate = vm.ScheduledDate;
            tripActivity.EstimatedCost = vm.EstimatedCost;

            tripActivityRepo.Update(tripActivity);
            await tripActivityRepo.SaveChangesAsync();
        }

        public async Task AddTripAsync(TripCreateViewModel vm, Guid userId)
        {
            var trip = new Trip
            {
                UserId = userId,
                DestinationCity = vm.DestinationCity,
                TripCurrency = vm.TripCurrency,
                StartDate = vm.StartDate,
                EndDate = vm.EndDate,
            };

            await tripRepo.AddAsync(trip);
            await tripRepo.SaveChangesAsync();
        }

        public async Task<TripActivityCreateViewModel?> FindTripActivityAsync(Guid tripId, Guid activityId)
        {
            var tripActivity = await tripActivityRepo.GetAllAttached()
                .FirstOrDefaultAsync(ta => ta.TripId == tripId && ta.ActivityId == activityId);

            if (tripActivity == null)
            {
                throw new NullReferenceException("There is no TripActivity with these IDs!");
            }

            return new TripActivityCreateViewModel
            {
                TripId = tripId,
                ActivityId = activityId,
                ScheduledDate = tripActivity.ScheduledDate,
                EstimatedCost = tripActivity.EstimatedCost,
            };
        }

        public async Task<IEnumerable<TripIndexViewModel>> GetFilteredTripsAsync(string destination, int pageNumber = 1, int pageSize = 3)
        {
            var query = tripRepo.GetAllAttached();

            if (!string.IsNullOrEmpty(destination))
            {
                query = query.Where(t => EF.Functions.Like(t.DestinationCity, $"%{destination}%"));
            }

            return await query
                .Select(t => new TripIndexViewModel
                {
                    Id = t.Id,
                    DestinationCity = t.DestinationCity,
                    TripCurrency = t.TripCurrency,
                    StartDate = t.StartDate,
                    EndDate = t.EndDate,

                }).Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalPagesAsync(int pageSize = 3)
        {
            int totalTrips = await tripRepo.GetAllAttached().CountAsync();

            return (int)Math.Ceiling(totalTrips / (double)pageSize);
        }

        public async Task<TripEditViewModel?> GetTripByIdAsync(Guid id)
        {
            var trip = await tripRepo.GetAllAttached()
                .Select(t => new TripEditViewModel
                {
                    Id = t.Id,
                    DestinationCity = t.DestinationCity,
                    TripCurrency = t.TripCurrency,
                    StartDate = t.StartDate,
                    EndDate = t.EndDate,
                }).FirstOrDefaultAsync();

            if (trip == null)
            {
                throw new NullReferenceException("There is no trip with this ID!");
            }

            return trip;
        }

        public async Task<TripDetailsViewModel?> GetTripWithActivitiesAsync(Guid id)
        {
            var trip = await tripRepo.GetAllAttached()
                .Select(t => new TripDetailsViewModel
                {
                    Id = t.Id,
                    DestinationCity = t.DestinationCity,
                    TripCurrency = t.TripCurrency,
                    StartDate = t.StartDate,
                    EndDate = t.EndDate,
                    Activities = t.TripsActivities.Select(ta => new TripActivityIndexViewModel
                    {
                        ActivityId = ta.ActivityId,
                        Title = ta.Activity.Title,
                        ScheduledDate = ta.ScheduledDate,
                        EstimatedCost = ta.EstimatedCost
                    }).ToList(),
                }).FirstOrDefaultAsync(t => t.Id == id);

            if (trip == null)
            {
                throw new NullReferenceException("There is no trip with this ID!");
            }

            return trip;
        }

        public async Task RemoveActivityFromTripAsync(Guid tripId, Guid activityId)
        {
            var tripActivity = await tripActivityRepo.GetAllAttached()
                .FirstOrDefaultAsync(ta => ta.TripId == tripId && ta.ActivityId == activityId);

            if (tripActivity == null)
            {
                throw new NullReferenceException("There is no TripActivity with this ID!");
            }

            tripActivityRepo.Remove(tripActivity);
            await tripActivityRepo.SaveChangesAsync();
        }

        public async Task RemoveTripAsync(Guid id)
        {
            var trip = await tripRepo.GetByIdAsync(id);

            if (trip == null)
            {
                throw new NullReferenceException("There is no trip with this ID!");
            }

            tripRepo.Remove(trip);
            await tripRepo.SaveChangesAsync();
        }

        public async Task UpdateTripAsync(TripEditViewModel vm)
        {
            var trip = await tripRepo.GetByIdAsync(vm.Id);

            if (trip == null)
            {
                throw new NullReferenceException("There is no trip with this ID!");
            }

            trip.DestinationCity = vm.DestinationCity;
            trip.TripCurrency = vm.TripCurrency;
            trip.StartDate = vm.StartDate;
            trip.EndDate = vm.EndDate;

            tripRepo.Update(trip);
            await tripRepo.SaveChangesAsync();
        }
    }
}