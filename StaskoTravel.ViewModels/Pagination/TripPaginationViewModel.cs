using StaskoTravel.ViewModels.Trip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoTravel.ViewModels.Pagination
{
    public class TripPaginationViewModel
    {
        public List<TripIndexViewModel> Trips { get; set; } = new();

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }
    }
}
