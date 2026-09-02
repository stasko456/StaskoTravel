using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoTravel.ViewModels.Trip
{
    public class TripEditViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Destination city is required!")]
        [StringLength(100, ErrorMessage = "The destination city can be 100 characters long!")]
        public string DestinationCity { get; set; } = null!;

        [Required(ErrorMessage = "Destination city is required!")]
        [StringLength(3, ErrorMessage = "The trip currency has to be exactly 3 characters long!")]
        public string TripCurrency { get; set; } = null!;

        public DateOnly StartDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public DateOnly EndDate { get; set; }
    }
}
