using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStats.Data;
using BookStats.Models;
using BookStats.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStats.Controllers
{
    public class AuthController : Controller
    {
        readonly IRepository repository;
        readonly SignInManager<User> signInManager;

        public AuthController(IRepository repo, SignInManager<User> signin)
        {
            repository = repo;
            signInManager = signin;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    UserName = model.UserName,
                    Name = model.Name,
                    Surname = model.Surname,
                    Email = model.Email,
                };
                var result = await repository.UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return Redirect("~/");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            UserLoginViewModel model = new UserLoginViewModel
            {
                ReturnURL = returnUrl
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(
                    model.UserName,
                    model.Password, 
                    model.RememberMe, 
                    false);
                if (result.Succeeded)
                {
                    var returnUrl = model.ReturnURL ?? Url.Content("~/");
                    return Redirect(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid password or username");
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Redirect("~/");
        }
    }
}