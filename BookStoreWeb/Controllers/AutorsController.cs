using BookStoreWeb.Model.Entities;
using BookStoreWeb.Models.Interfaces;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookStoreWeb.Controllers
{
    public class AutorsController : Controller
    {
        private readonly IAutorRepository _autorRepository;
        public AutorsController(IAutorRepository repository)
        {
            _autorRepository = repository;
        }
        // GET: AutorController
        public async Task<IActionResult> Index()
        {
            try
            {
                var operation = await _autorRepository.GetAutors();
                ViewBag.Status = operation.State;
                ViewBag.Message = operation.Message;
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            return View();
        }
        public async Task<ActionResult> Autors([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                var operation = await _autorRepository.GetAutors();
                var autors = JsonConvert.DeserializeObject<IEnumerable<Autor>>(operation.Response);
                return Json(autors.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return Json("");
            }
        }

        // GET: AutorController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var autor = await _autorRepository.GetAutorById(id);
            return View(autor);
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: AutorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Name,Lastname,Birthdate,City,Email")] Autor autor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var operation = await _autorRepository.InsertAutor(autor);
                    if (operation.State.Equals("fail"))
                    {
                        throw new Exception(operation.Message);
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(autor);
            }
            catch(Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // POST: AutorController/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit([DataSourceRequest] DataSourceRequest request, Autor autor)
        {
            if (autor != null && ModelState.IsValid)
            {
                await _autorRepository.EditAutor(autor.AutorId, autor);
            }

            return Json(new[] { autor }.ToDataSourceResult(request, ModelState));
        }

        // POST: AutorController/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete([DataSourceRequest] DataSourceRequest request, Autor autor)
        {
            try
            {
                var operation = await _autorRepository.DeleteAutor(autor.AutorId);
                ViewBag.Message(operation.Message);
                if (operation.State.Equals("fail"))
                {
                    throw new Exception(operation.Message);
                }
                return Json(new[] { autor }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View("Index");
            }
        }
    }
}
