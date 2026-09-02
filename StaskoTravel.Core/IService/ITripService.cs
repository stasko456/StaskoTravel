using StaskoTravel.ViewModels.Trip;
using StaskoTravel.ViewModels.TripActivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoTravel.Core.IService
{
    public interface ITripService
    {
        Task<IEnumerable<TripIndexViewModel>> GetFilteredTripsAsync(string destination, int pageNumber = 1, int pageSize = 3);

        Task<TripEditViewModel?> GetTripByIdAsync(Guid id);

        Task AddTripAsync(TripCreateViewModel vm, Guid userId);

        Task UpdateTripAsync(TripEditViewModel vm);

        Task RemoveTripAsync(Guid id);

        Task<int> GetTotalPagesAsync(int pageSize = 3);

        Task<TripDetailsViewModel?> GetTripWithActivitiesAsync(Guid id);

        Task AddActivityToTripAsync(Guid tripId, Guid activityId);

        Task RemoveActivityFromTripAsync(Guid tripId, Guid activityId);

        Task AddSepcificationsAsync(TripActivityCreateViewModel vm);

        Task<TripActivityCreateViewModel?> FindTripActivityAsync(Guid tripId, Guid activityId);
    }
}