using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStats.Models.SharedModel;

namespace BookStats.Models
{
    public class BookGenre : Entity
    {
        public int BookId { get; set; }
        public int GenreId { get; set; }
        public virtual Book Book { get; set; }
        public virtual Genre Genre { get; set; }
    }
}
