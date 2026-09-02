using StaskoTravel.ViewModels.Activity;
using StaskoTravel.ViewModels.TripActivity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoTravel.ViewModels.Trip
{
    public class TripDetailsViewModel
    {
        public Guid Id { get; set; }

        public string DestinationCity { get; set; } = null!;

        public string TripCurrency { get; set; } = null!;

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public ICollection<TripActivityIndexViewModel> Activities { get; set; } = new List<TripActivityIndexViewModel>();
    }
}