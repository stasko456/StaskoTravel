using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.Extensions.Options;
using StaskoTravel.Core.IService;
using StaskoTravel.DataAccess.Repository;
using StaskoTravel.Models.Entities;
using StaskoTravel.ViewModels.Activity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoTravel.Core.Service
{
    public class ActivityService : IActivityService
    {
        private readonly IRepository<Activity> activityRepo;

        public ActivityService(IRepository<Activity> _activityRepo)
        {
            this.activityRepo = _activityRepo;
        }

        public async Task AddActivityAsync(ActivityCreateViewModel vm)
        {
            var activity = new Activity
            {
                Title = vm.Title,
            };

            await activityRepo.AddAsync(activity);
            await activityRepo.SaveChangesAsync();
        }

        public async Task<ActivityEditViewModel?> GetActivityByIdAsync(Guid id)
        {
            var activity = await activityRepo.GetAllAttached()
                .Select(a => new ActivityEditViewModel
                {
                    Id = a.Id,
                    Title = a.Title,
                }).FirstOrDefaultAsync();

            if (activity == null)
            {
                throw new NullReferenceException("There is no activity with this ID!");
            }

            return activity;
        }

        public async Task<IEnumerable<ActivityIndexViewModel>> GetFilteredActivitiesAsync(string title, int pageNumber = 1, int pageSize = 6)
        {
            var query = activityRepo.GetAllAttached();

            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(a => EF.Functions.Like(a.Title, $"%{title}%"));
            }

            return await query
                .Select(a => new ActivityIndexViewModel
                {
                    Id = a.Id,
                    Title = a.Title,
                }).Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<ActivityIndexViewModel>> GetFirst5FilteredAsync(string title)
        {
            return await activityRepo.GetAllAttached()
                .Select(a => new ActivityIndexViewModel
                {
                    Id = a.Id,
                    Title = a.Title,
                }).ToListAsync();
        }

        public async Task<int> GetTotalPagesAsync(int pageSize = 6)
        {
            int totalActivities = await activityRepo.GetAllAttached().CountAsync();

            return (int)Math.Ceiling(totalActivities / (double)pageSize);
        }

        public async Task RemoveActivityAsync(Guid id)
        {
            var activity = await activityRepo.GetByIdAsync(id);

            if (activity == null)
            {
                throw new NullReferenceException("There is no activity with this ID!");
            }

            activityRepo.Remove(activity);
            await activityRepo.SaveChangesAsync();
        }

        public async Task UpdateActivityAsync(ActivityEditViewModel vm)
        {
            var activity = await activityRepo.GetByIdAsync(vm.Id);

            if (activity == null)
            {
                throw new NullReferenceException("There is no activity with this ID!");
            }

            activity.Title = vm.Title;

            activityRepo.Update(activity);
            await activityRepo.SaveChangesAsync();
        }
    }
}