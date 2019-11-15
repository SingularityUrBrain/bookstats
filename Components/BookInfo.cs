using BookStats.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStats.Components
{
    public class BookInfo : ViewComponent
    {
        private readonly ApplicationDbContext context;
        public BookInfo(ApplicationDbContext ctx)
        {
            context = ctx;
        }
        public async Task<IViewComponentResult> InvokeAsync(int id) =>
            View(await context.Books.FindAsync(id));
    }
}
