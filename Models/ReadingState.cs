using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStats.Models
{
    public class ReadingState
    {
        public string UserId { get; set; }

        public int BookId { get; set; }

        public virtual User User { get; set; }

        public virtual Book Book { get; set; }

        public State State { get; set; } = State.NotReading;
    }
    public enum State
    {
        Reading,
        GoingToRead,
        StoppedReading,
        NotReading
    }
}
