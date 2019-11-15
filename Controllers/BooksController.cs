using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStats.Data;
using BookStats.Models;
using BookStats.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookStats.Controllers
{
    public class BooksController : Controller
    {
        private readonly IRepository repository;
        public BooksController(IRepository repo)
        {
            repository = repo;
        }

        public IActionResult Index()
        {
            return View(repository.Books.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new BookCreateViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await repository.UserManager.FindByNameAsync(User.Identity.Name);
                if (user is null)
                {
                    throw new InvalidOperationException("Smth went wrong. There is no current user.");
                }
                Book book = new Book
                {
                    Author = new Author {FirstName = "Author FirstName", LastName="Author SecondName", Age=52, Country="Country"}, // TODO
                    Title = model.Title,
                    PagesNumber = model.PagesNumber,
                    PublicationYear = model.PublicationYear,
                    Description = model.Description
                };
                await repository.Context.Books.AddAsync(book);
                await repository.Context.SaveChangesAsync();
                
            }
            return RedirectToAction("Index", "Books");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int bookId)
        {
            var book = repository.Books.FirstOrDefault(b => b.Id == bookId);
            if (book is null)
            {
                return NotFound();
            }
            repository.Context.Books.Remove(book);
            await repository.Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}