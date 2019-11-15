using BookStats.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStats.ViewModels
{
    public class BookCreateViewModel
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public Author Author { get; set; }

        [Required]
        public int PagesNumber { get; set; }

        [Required]
        [Display(Name = "Publication Year")]
        public int PublicationYear { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}
