using BookStats.Models.SharedModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStats.Models
{
    public class Genre : Entity
    {
        [StringLength(50)]
        public string GenreName { get; set; }
        public virtual ICollection<BookGenre> BookGenres { get; set; }
    }
}
