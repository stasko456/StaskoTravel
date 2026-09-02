using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoTravel.ViewModels.TripActivity
{
    public class TripActivityCreateViewModel
    {
        public Guid TripId { get; set; }

        public Guid ActivityId { get; set; }

        [Required]
        public DateOnly ScheduledDate { get; set; }

        [Required]
        public decimal EstimatedCost { get; set; }
    }
}