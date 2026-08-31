using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoTravel.ViewModels.Activity
{
    public class ActivityEditViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Title is required!")]
        [StringLength(150, MinimumLength = 5, ErrorMessage = "Title has to be between 5 and 100 characters long!")]
        public string Title { get; set; } = null!;
    }
}