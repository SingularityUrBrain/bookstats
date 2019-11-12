using BookStats.Models.SharedModel;

namespace BookStats.Models
{
    public class BookMark : Entity
    {
        public string UserId { get; set; }
        
        public int? BookId { get; set; }

        public virtual User User { get; set; }

        public virtual Book Book { get; set; }
    }
}
