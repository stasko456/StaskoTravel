using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoTravel.ViewModels.Trip
{
    public class TripCreateViewModel
    {
        [Required(ErrorMessage = "Destination city is required!")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "The destination city has to be between 5 and 100 characters long!")]
        public string DestinationCity { get; set; } = null!;

        [Required(ErrorMessage = "Destination city is required!")]
        [StringLength(3, ErrorMessage = "The trip currency has to be between 3 characters long!")]
        public string TripCurrency { get; set; } = null!;

        public DateOnly StartDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public DateOnly EndDate { get; set; } = DateOnly.FromDateTime(DateTime.Today.AddDays(1));
    }
}