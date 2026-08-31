using StaskoTravel.ViewModels.Activity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoTravel.Core.IService
{
    public interface IActivityService
    {
        Task<IEnumerable<ActivityIndexViewModel>> GetFilteredActivitiesAsync(string title, int pageNumber = 1, int pageSize = 5);

        Task<ActivityEditViewModel?> GetActivityByIdAsync(Guid id);

        Task AddActivityAsync(ActivityCreateViewModel vm);

        Task UpdateActivityAsync(ActivityEditViewModel vm);

        Task RemoveActivityAsync(Guid id);

        Task<int> GetTotalPagesAsync(int pageSize = 5);
    }
}