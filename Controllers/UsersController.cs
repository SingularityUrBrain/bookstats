using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStats.Data;
using BookStats.Models;
using BookStats.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStats.Controllers
{
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly IRepository repository;
        private readonly SignInManager<User> signInManager;

        public UsersController(IRepository repo, SignInManager<User> manager)
        {
            repository = repo;
            signInManager = manager;
        }

        public async Task<IActionResult> Index() =>
            View(await repository.UserManager.Users.ToListAsync());
        

        [HttpGet]
        public async Task<IActionResult> Delete(string userID)
        {
            User user = await repository.UserManager.FindByIdAsync(userID);
            if (user is null)
            {
                return NotFound($"User with ID '{userID}' was not found");
            }
            else if (user.UserName == User.Identity.Name)
            {
                await signInManager.SignOutAsync();
            }
            var result = await repository.UserManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Exception caused deleting user, ID: '{userID}'");
            }
            return RedirectToAction("Index");
        }
    }
}