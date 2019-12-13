using BookStats.Models;
using Microsoft.AspNetCore.Http;
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
        [RegularExpression(@"^[1-9]\d*", ErrorMessage = "Invalid the number of pages")]
        public int PagesNumber { get; set; }

        [Required]
        [RegularExpression(@"[12]\d{3}", ErrorMessage = "Invalid publication year")]
        public int PublicationYear { get; set; }

        public string Publisher { get; set; }

        public IFormFile Cover { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}
