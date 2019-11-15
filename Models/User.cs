using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace BookStats.Models
{
    public class User : IdentityUser
    {
        [MaxLength(64)]
        public string Name { get; set; }

        [MaxLength(64)]
        public string Surname { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime RegisterAt { get; set; } = DateTime.Now;
        public string PhotoUrl => System.IO.File.Exists($"/pictures/user/{Id}.jpg") ?
            $"/pictures/user/{Id}.jpg" : $"/pictures/default.png";

        public virtual ICollection<BookMark> BookMarks { get; set; }
        public virtual ICollection<BookNote> BookNotes { get; set; }
        public virtual ICollection<ReadingState> ReadingStates { get; set; } // ???

    }
}
