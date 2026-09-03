using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using StaskoTravel.ViewModels.Validation;

namespace StaskoTravel.Models.Entities
{
    public class Trip
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string DestinationCity { get; set; } = null!;

        [Required]
        [MaxLength(3)]
        public string TripCurrency { get; set; } = null!;

        [DataType(DataType.Date)]
        public DateOnly StartDate { get; set; }

        [DataType(DataType.Date)]
        [ExactDurationDays("StartDate", ErrorMessage = "End date must be after the start date.")]
        [DateGreaterThan("StartDate", ErrorMessage = "The difference betwee the two dates cannot be more than 14 days")]
        public DateOnly EndDate { get; set; }

        public ICollection<TripActivity> TripsActivities { get; set; } = new List<TripActivity>();
    }
}