using BookStats.Models.SharedModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStats.Models
{
    public class Genre : Entity
    {
        public Genre()
        {
            Books = new HashSet<Book>();
        }
        [StringLength(50)]
        public string GenreName { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}
