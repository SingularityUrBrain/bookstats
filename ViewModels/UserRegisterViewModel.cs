using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStats.ViewModels
{
    public class UserRegisterViewModel
    {
        [Required]
        [RegularExpression(@"^[A-Za-zА-яёЁ]\w+$",
            ErrorMessage = "Invalid Username")]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-zА-яёЁ]+$",
            ErrorMessage = "Invalid Name")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [RegularExpression(@"^[A-Za-zА-яёЁ]+$",
            ErrorMessage = "Invalid Surname")]
        [Required]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The passwords don't match")]
        public string ConfirmPassword { get; set; }
    }
}
