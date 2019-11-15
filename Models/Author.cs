using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BookStats.Models.SharedModel;
using System.Linq;
using System.Threading.Tasks;

namespace BookStats.Models
{
    public class Author : Entity
    {

        [Required(ErrorMessage = "First Name Required")]
        [Display(Name = "First Name")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name Required")]
        [Display(Name = "Last Name")]
        [StringLength(50)]
        public string LastName { get; set; }


        [Display(Name = "Country")]
        [StringLength(80)]
        public string Country { get; set; }
        
        public int Age { get; set; }

        public string PhotoUrl { get; set; } // ??

        public virtual ICollection<Book> Books { get; set; }
    }
}
