using Bookit.Data;
using Bookit.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bookit.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db;
        public BookController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var model =_db.Books;
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Book model)
        {
            _db.Books.Add(model);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var book = _db.Books.Find(id);
            return View(book);
        }
        [HttpPost]
        public IActionResult Edit(Book model)
        {
            _db.Entry<Book>(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int id)
        {
            var book = _db.Books.Find(id);
            return View(book);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var book = _db.Books.Find(id);
            return View(book);
        }
        [HttpPost]
        public IActionResult Delete(Book model)
        {
            _db.Entry<Book>(model).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
