using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BookStats.Data;
using BookStats.Models;
using BookStats.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookStats.Controllers
{
    public class PagedData<T> where T : class
    {
        public IEnumerable<T> Data { get; set; }
        public int NumberOfPages { get; set; }
        public int CurrentPage { get; set; }
    }

    public class BooksController : Controller
    {
        private readonly IRepository repository;
        public const int pageSize = 7;

        public BooksController(IRepository repo)
        {
            repository = repo;
        }

        public ActionResult Index(int page=1)
        {
            var books = new PagedData<Book>
            {
                Data = repository.Books.Skip(pageSize * (page - 1)).Take(pageSize).ToList(),
                NumberOfPages = Convert.ToInt32(Math.Ceiling((double)repository.Books.Count() / pageSize)),
                CurrentPage = page
            };

            return View(books);
        }

        public ActionResult BookListAjax(int page)
        {
            var books = new PagedData<Book>
            {
                Data = repository.Books.Skip(pageSize*(page-1)).Take(pageSize).ToList(),
                NumberOfPages = Convert.ToInt32(Math.Ceiling((double)repository.Books.Count() / pageSize)),
                CurrentPage = page
            };
            return PartialView("BookListAjax", books);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new BookCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
                    Author = new Author { FirstName = "Author FirstName", LastName = "Author SecondName", Age = 52, Country = "Country" }, // TODO
                    Title = model.Title,
                    Publisher = model.Publisher,
                    PagesNumber = model.PagesNumber,
                    PublicationYear = model.PublicationYear,
                    Description = model.Description
                };
                if (model.Cover != null && model.Cover.Length > 0)
                {
                    string dir = "wwwroot";
                    string path = $"/pictures/book/{model.Cover.FileName}";
                    using var stream = new FileStream(dir + path, FileMode.OpenOrCreate);
                    await model.Cover.CopyToAsync(stream);
                    book.ImgUrl = path;
                }
                await repository.Context.Books.AddAsync(book);
                await repository.Context.SaveChangesAsync();
                
            }
            else
            {
                return View(model);
            }
            return RedirectToAction("Index");
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

        [HttpGet]
        public async Task<IActionResult> Details(Int32 bookId)
        {
            var book = await repository.Context.Books.FindAsync(bookId);
            return View(book);
        }
    }
}