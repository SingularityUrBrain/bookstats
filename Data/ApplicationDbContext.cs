using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BookStats.Models;

namespace BookStats.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Book> Books { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<BookNote> BookNotes { get; set; }

        public DbSet<BookMark> BookMarks { get; set; }

        public DbSet<ReadingState> ReadingStates { get; set; }

        public DbSet<Genre> Genres { get; set; }
    }
}
