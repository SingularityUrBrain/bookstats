using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStats.ViewModels
{
    public class UserEditViewModel
    {
        public string Id { get; set; }

        [Required]
        [RegularExpression(
            @"^(?=.{2,32}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$",
            ErrorMessage = "Username must comply with the following rules:\n" +
            "1. username is 2-32 chars long;\n2. no _ or . at the beginning;\n" +
            "3. no __ or _. or .. inside;\n4. no _ or . at the end."
            )]
        [Display(Name = "Username")]
        public string UserName { get; set; }


        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public IFormFile Photo { get; set; }

        public string PhotoPath => System.IO.File.Exists($"/pictures/user/{Id}.jpg") ?
            $"/pictures/user/{Id}.jpg" : $"/pictures/default.png";  // TODO: add default.png

        public string StatusMessage { get; internal set; }
    }
}
