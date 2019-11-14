using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BookStats.Data;
using BookStats.Models;
using BookStats.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStats.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        readonly IRepository repository;
        readonly SignInManager<User> signInManager;

        public AccountController(IRepository repo, SignInManager<User> signin)
        {
            repository = repo;
            signInManager = signin;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string userName = null) =>
            View(new UserDeatailsViewModel
            {
                User = await repository.UserManager.FindByNameAsync(userName ?? User.Identity.Name),
                IsActive = userName == User.Identity.Name
            });

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            User user = await repository.UserManager.FindByNameAsync(User.Identity.Name);
            if (user is null)
                return NotFound();

            UserEditViewModel model = new UserEditViewModel
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
                var user = await repository.UserManager.FindByNameAsync(User.Identity.Name);
                if (user != null)
                {
                    user.Name = model.Name;
                    user.Surname = model.Surname;
                    user.UserName = model.UserName;
                    user.Email = model.Email;
                    var result = await repository.UserManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        model.StatusMessage = "Your profile has been updated";
                        if (model.Photo != null)
                        {
                            using var stream = new FileStream($"wwwroot/pictures/user/{user.Id}.jpg", FileMode.OpenOrCreate);
                            await model.Photo.CopyToAsync(stream);
                        }
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                    await signInManager.RefreshSignInAsync(user);
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete()
        {
            User user = await repository.UserManager.FindByNameAsync(User.Identity.Name);
            if (user is null)
            {
                return NotFound();
            }
            UserDeleteViewModel model = new UserDeleteViewModel
            {
                UserName = user.UserName,
                Id = user.Id
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UserDeleteViewModel model)
        {
            var user = await repository.UserManager.FindByNameAsync(User.Identity.Name);
            if (user is null)
            {
                return NotFound($"User with ID '{model.Id}' was not found");
            }
            if (!await repository.UserManager.CheckPasswordAsync(user, model.ConfirmPassword))
            {
                ModelState.AddModelError(string.Empty, "Password is not correct");
                return View(model);
            }
            var result = await repository.UserManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Exception caused deleting user, ID: '{model.Id}'");
            }
            await signInManager.SignOutAsync();
            return Redirect("~/");
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            var user = await repository.UserManager.FindByNameAsync(User.Identity.Name);
            if (user is null)
            {
                return NotFound($"User with username '{User.Identity.Name}' was not found");
            }
            UserChangePasswordViewModel model = new UserChangePasswordViewModel
            {
                Id = user.Id,
                Username = user.UserName
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(UserChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await repository.UserManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    if (!await repository.UserManager.CheckPasswordAsync(user, model.ConfirmPassword))
                    {
                        ModelState.AddModelError(string.Empty, "Password is not correct");
                        return View(model);
                    }
                    var result = await repository.UserManager.ResetPasswordAsync(
                        user,
                        await repository.UserManager.GeneratePasswordResetTokenAsync(user),
                        model.NewPassword);
                    if (result.Succeeded)
                    {
                        ModelState.AddModelError(string.Empty, "Your password has been changed");
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
    }
}