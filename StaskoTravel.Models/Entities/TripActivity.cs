using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoTravel.Models.Entities
{
    [PrimaryKey(nameof(TripId), nameof(ActivityId))]
    public class TripActivity
    {
        [Required]
        [ForeignKey(nameof(Trip))]
        public Guid TripId { get; set; }
        public Trip Trip { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Activity))]
        public Guid ActivityId { get; set; }
        public Activity Activity { get; set; } = null!;

        public DateOnly ScheduledDate { get; set; }

        public decimal EstimatedCost { get; set; }
    }
}