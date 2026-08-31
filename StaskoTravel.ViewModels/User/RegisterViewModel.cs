using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoTravel.ViewModels.User
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "First name is required!")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "First name has to be between 5 and 50 characters!")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Last name is required!")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Last name has to be between 5 and 50 characters!")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "Username is required!")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Username has to be between 5 and 20 characters!")]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = "Email address is required!")]
        [EmailAddress]
        [StringLength(60, MinimumLength = 10, ErrorMessage = "Email Address must be between 10 and 60 characters!")]
        public string EmailAddress { get; set; } = null!;

        [Required(ErrorMessage = "Password is required!")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Password must be between 5 and 20 characters!")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Password conformation is required!")]
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;
    }
}