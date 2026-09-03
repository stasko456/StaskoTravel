using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
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

        [DataType(DataType.Date)]
        public DateOnly ScheduledDate { get; set; }

        [DataType(DataType.Currency)]
        [Range(typeof(decimal), "0.01", "10000.00", ErrorMessage = "Cost must be between $0.01 and $10,000.00.")]
        public decimal EstimatedCost { get; set; }
    }
}