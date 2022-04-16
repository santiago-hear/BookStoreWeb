using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BookStoreWeb.Model.Entities;
using Kendo.Mvc.UI;
using BookStoreWeb.Models.Interfaces;
using Kendo.Mvc.Extensions;
using Newtonsoft.Json;

namespace BookStoreWeb.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAutorRepository _autorRepository;

        public BooksController(IBookRepository bookRepository, IAutorRepository autorRepository)
        {
            _bookRepository = bookRepository;
            _autorRepository = autorRepository;
        }
        // GET: BooksController 
        public async Task<ActionResult> Index()
        {
            try
            {
                var operation = await _bookRepository.GetBooks();
                ViewBag.Status = operation.State;
                ViewBag.Message = operation.Message;
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            return View();
        }
        public async Task<ActionResult> Books([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                var operation = await _bookRepository.GetBooks();
                var books = JsonConvert.DeserializeObject<IEnumerable<Book>>(operation.Response);
                return Json(books.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return Json("");
            }
        }

        // GET: BooksController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var book = await _bookRepository.GetBookById(id);
            return View(book);
        }

        public async Task<ActionResult> Create()
        {
            try
            {
                List<SelectListItem> autors = await selectAutors();
                ViewBag.autors = autors;
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            return View();
        }

        // POST: BooksController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection, [Bind("Title,Year,Category,Autor")] Book book)
        {
            try
            {
                collection.TryGetValue("Title", out var title);
                collection.TryGetValue("Year", out var year);
                collection.TryGetValue("Category", out var category);
                collection.TryGetValue("Autor", out var autor);
                var operationaAutor = await _autorRepository.GetAutorById(int.Parse(autor));
                var Sautor = JsonConvert.DeserializeObject<Autor>(operationaAutor.Response);
                Book Newbook = new Book()
                {
                    Title = title,
                    Year = year,
                    Category = category,
                    Autor = Sautor
                };
                var operation = await _bookRepository.InsertBook(Newbook);
                if (operation.State.Equals("fail"))
                {
                    throw new Exception(operation.Message);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                ViewBag.autors = await selectAutors();
                return View();
            }
        }

        // POST: BookController/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit([DataSourceRequest] DataSourceRequest request, Book book)
        {
            if (book != null && ModelState.IsValid)
            {
                await _bookRepository.EditBook(book.BookId, book);
            }

            return Json(new[] { book }.ToDataSourceResult(request, ModelState));
        }

        // POST: BookController/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete([DataSourceRequest] DataSourceRequest request, Book book)
        {
            try
            {
                var operation = await _bookRepository.DeleteBook(book.BookId);
                ViewBag.Message(operation.Message);
                if(operation.State.Equals("fail"))
                {
                    throw new Exception(operation.Message);
                }
                return Json(new[] { book }.ToDataSourceResult(request, ModelState));
            }
            catch(Exception ex)
            {
                ViewBag.Message = ex;
                return RedirectToAction(nameof(Index));
            }
        }
        private async Task<List<SelectListItem>> selectAutors()
        {
            var operation = await _autorRepository.GetAutors();
            var Allautors = JsonConvert.DeserializeObject<IEnumerable<Autor>>(operation.Response);
            List<SelectListItem> autors = Allautors.ToList().ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.Name + " " + a.Lastname,
                    Value = a.AutorId.ToString(),
                    Selected = false
                };
            });
            return autors;
        }
    }
}
