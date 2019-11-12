using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BookStats.Models.SharedModel;

namespace BookStats.Models
{
    public class BookNote : Entity
    {
        public int UserId { get; set; }
        public int BookId { get; set; }

        public virtual User User { get; set; }
        public virtual Book Book { get; set; }

        [MaxLength(4096)]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }
    }
}
