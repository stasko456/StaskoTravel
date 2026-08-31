using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoTravel.ViewModels.Trip
{
    public class TripIndexViewModel
    {
        public Guid Id { get; set; }

        public string DestinationCity { get; set; } = null!;

        public string TripCurrency { get; set; } = null!;

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }
    }
}