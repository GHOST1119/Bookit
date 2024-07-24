using Bookit.Data;
using Bookit.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookit.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db;
        public BookController(ApplicationDbContext db)
        {
            _db = db;
        }
        // GET: Book
        public async Task<IActionResult> Index()
        {
            var categorylist =_db.Categorys.ToListAsync();
            ViewData["categorylist"] = categorylist;
            return View(await _db.Books.ToListAsync());
        }
        // GET: Gallery
        [AllowAnonymous]
        public async Task<IActionResult> Gallery()
        {
            return View(await _db.Books.ToListAsync());
        }
        // GET: Create
        [HttpGet]
        public IActionResult Create()
        {
            var categorylist = _db.Categorys.ToListAsync();
            ViewData["categorylist"] = categorylist;
            return View();
        }
        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book model)
        {
            _db.Books.Add(model);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // GET: Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var book = await _db.Books.FirstOrDefaultAsync(m => m.Id == id);
            var categorylist = _db.Categorys.ToListAsync();
            ViewData["categorylist"] = categorylist;
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }
        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Book model)
        {
            _db.Entry<Book>(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // GET: More
        [AllowAnonymous]
        public async Task<IActionResult> More(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var book = await _db.Books.FirstOrDefaultAsync(m => m.Id == id);
            var categorylist = _db.Categorys.ToListAsync();
            ViewData["categorylist"] = categorylist;
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }
        // GET: Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var book = await _db.Books.FirstOrDefaultAsync(m => m.Id == id);
            var categorylist =_db.Categorys.ToListAsync();
            ViewData["categorylist"] = categorylist;
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }
        // GET: Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var book = await _db.Books.FirstOrDefaultAsync(m => m.Id == id);
            var categorylist =_db.Categorys.ToListAsync();
            ViewData["categorylist"] = categorylist;
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }
        // POST: Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Book model)
        {
            _db.Entry<Book>(model).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
