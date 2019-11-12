using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStats.Models;
using Microsoft.AspNetCore.Identity;

namespace BookStats.Data
{
    public class Repository : IRepository
    {
        public Repository(
            ApplicationDbContext ctx,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            Context = ctx;
            UserManager = userManager;
            RoleManager = roleManager;
        }

        public IQueryable<Book> Books => Context.Books;
        public IQueryable<Author> Authors => Context.Authors;
        public IQueryable<BookNote> BookNotes => Context.BookNotes;
        public IQueryable<BookMark> BookMarks => Context.BookMarks;
        public IQueryable<ReadingState> ReadingStates => Context.ReadingStates;
        public IQueryable<Genre> Genres => Context.Genres;

        public IQueryable<User> Users => Context.Users;

        public UserManager<User> UserManager { get; }

        public RoleManager<IdentityRole> RoleManager { get; }

        public ApplicationDbContext Context { get; }
    }
}
