using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace BookStats.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            BookMarks = new HashSet<BookMark>();
            BookNotes = new HashSet<BookNote>(); // ???
        }

        [MaxLength(64)]
        public string Name { get; set; }

        [MaxLength(64)]
        public string Surname { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public string PhotoUrl { get; set; } // ??
        
        public virtual ICollection<BookMark> BookMarks { get; set; }
        public virtual ICollection<BookNote> BookNotes { get; set; }
        public virtual ICollection<ReadingState> ReadingStates { get; set; } // ???

    }
}
