using System;
using BookStats.Models.SharedModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace BookStats.Models
{
    public class Book : Entity
    {
        private static double rating;

        [StringLength(100)]
        public string Title { get; set; }

        [Display(Name = "Author")]
        public int AuthorId { get; set; }

        public int PagesNumber { get; set; }


        [Display(Name = "Publication Year")]
        public int PublicationYear { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [StringLength(50)]
        public string ImgUrl => System.IO.File.Exists($"/pictures/book/{Id}.jpg") ?
            $"/pictures/book/{Id}.jpg" : $"/pictures/default_book.jpg";
            
        [StringLength(50)]
        public string Publisher { get; set; }

        public virtual Author Author { get; set; }

        public int Votes { get; private set; } = 0;
        public double Rating {
            get => rating;
            set {
                if (Votes == 0)
                {
                    rating = value;
                }
                else
                {
                    rating = Math.Round((rating + value) / 2, 2);
                }
                Votes += 1;
            }
        }

        public virtual ICollection<BookGenre> BookGenres { get; set; }
    }
}
