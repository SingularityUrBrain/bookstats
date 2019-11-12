using System.Linq;
using BookStats.Models;
using Microsoft.AspNetCore.Identity;

namespace BookStats.Data
{
    public interface IRepository
    {
        IQueryable<Book> Books { get; }

        IQueryable<Author> Authors { get; }

        IQueryable<BookNote> BookNotes { get; }

        IQueryable<BookMark> BookMarks { get; }

        IQueryable<ReadingState> ReadingStates { get; }

        IQueryable<Genre> Genres { get; }

        IQueryable<User> Users { get; }

        UserManager<User> UserManager { get; }

        RoleManager<IdentityRole> RoleManager { get; }

        ApplicationDbContext Context { get; }
    }
}
