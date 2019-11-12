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
        //public Author()
        //{
        //    Books = new HashSet<Book>();
        //}

        [Required(ErrorMessage = "First Name Required")]
        [Display(Name = "First Name")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name Required")]
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
