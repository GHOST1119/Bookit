using Bookit.Data;
using Bookit.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using System.Collections.Generic;
using System.IO;
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
            var categorylist = await _db.Categorys.ToListAsync();
            ViewData["categorylist"] = categorylist;
            return View(await _db.Books.ToListAsync());
        }
        // GET: Gallery
        [AllowAnonymous]
        [Route("book/gallery/{term?}")]
        public async Task<IActionResult> Gallery(string term = "")
        {
            dynamic model;
            if (string.IsNullOrEmpty(term))
            {
                model = await _db.Books.ToListAsync();
            }
            else
            {
                model = await _db.Books.Where(p => p.Name.Contains(term)).ToListAsync();
            }
            ViewData["term"] = term;
            return View(model);
        }
        // GET: Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categorylist = await _db.Categorys.ToListAsync();
            ViewData["categorylist"] = categorylist;
            return View();
        }
        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book model, IFormFile imageFile)
        {
            // Generate a unique file name
            string BookName = model.Name.Replace(" ", "_");
            string imageExtension = Path.GetExtension(imageFile.FileName);
            //string ImageName = BookName + imageExtension.Substring(imageExtension.LastIndexOf("."));                      
            // save in database
            Book bookModel = new Book()
            {
                Name = model.Name,
                CategoryId = model.CategoryId,
                Description = model.Description,
                Price = model.Price,
                Author = model.Author,
                Year = model.Year,
                Pages = model.Pages,
            };
            _db.Books.Add(bookModel);
            await _db.SaveChangesAsync();
            // update database
            int newBookId = bookModel.Id;
            // Define the path to save the image
            string ImageName = newBookId + "_" + BookName + imageExtension;
            var path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Books\" + ImageName);
            // Save the image file
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }
            bookModel.ImagePath = $"Books/{ImageName}";
            _db.Books.Update(bookModel);
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
            var categorylist = await _db.Categorys.ToListAsync();
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
            var categorylist = await _db.Categorys.ToListAsync();
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
            var categorylist =await _db.Categorys.ToListAsync();
            ViewData["categorylist"] = categorylist;
            if (book == null || categorylist == null)
            {
                return NotFound();
            }
            var viewModel = new BookDetailsViewModel
            {
                Book = book,
                Categories = categorylist
            };
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
            var categorylist =await _db.Categorys.ToListAsync();
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
