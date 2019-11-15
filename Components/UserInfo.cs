using BookStats.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStats.Components
{
    public class UserInfo : ViewComponent
    {
        private readonly ApplicationDbContext context;

        public UserInfo(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id) =>
            View(await context.Users.FindAsync(id));
    }

}
