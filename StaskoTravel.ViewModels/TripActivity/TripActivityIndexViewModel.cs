using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoTravel.ViewModels.TripActivity
{
    public class TripActivityIndexViewModel
    {
        public Guid ActivityId { get; set; }

        public string Title { get; set; } = null!;

        public DateOnly ScheduledDate { get; set; }

        public decimal EstimatedCost { get; set; }
    }
}