using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoTravel.ViewModels.User
{
    public class UserEditViewModel
    {
        [Required(ErrorMessage = "Home currency is required!")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Home currency has to be exactly 3 characters long!")]
        public string HomeCurrecny { get; set; } = null!;
    }
}