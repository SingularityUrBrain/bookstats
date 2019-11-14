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

        public async Task<IActionResult> Index()
        {
            return View(await repository.UserManager.Users.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string userID)
        {
            User user = await repository.UserManager.FindByIdAsync(userID);
            if (user is null)
            {
                return NotFound();
            }
            var model = new UserEditViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                UserName = user.UserName,
                Email = user.Email

            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await repository.UserManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.Name = model.Name;
                    user.Surname = model.Surname;
                    user.UserName = model.UserName;
                    user.Email = model.Email;
                    var result = await repository.UserManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        ModelState.AddModelError(string.Empty, "Profile has been updated");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return View(model);
        }

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