using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.Identity.Client;
using StaskoTravel.Core.IService;
using StaskoTravel.DataAccess.Repository;
using StaskoTravel.Models.Entities;
using StaskoTravel.ViewModels.Trip;
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

        public TripService(IRepository<Trip> _tripRepo)
        {
            this.tripRepo = _tripRepo;
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

        public async Task<IEnumerable<TripIndexViewModel>> GetFilteredTripsAsync(string destination, int pageNumber = 1, int pageSize = 5)
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

        public async Task<int> GetTotalPagesAsync(int pageSize = 5)
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