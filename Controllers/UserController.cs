using Bookit.Data;
using Bookit.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bookit.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;
        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var Users = _db.Users;
            return View(Users);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(User model)
        {
            _db.Users.Add(model);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var User = _db.Users.Find(id);
            return View(User);
        }
        [HttpPost]
        public IActionResult Edit(User model)
        {
            _db.Entry<User>(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int id)
        {
            var User = _db.Users.Find(id);
            return View(User);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var User = _db.Users.Find(id);
            return View(User);
        }
        [HttpPost]
        public IActionResult Delete(User model)
        {
            _db.Entry<User>(model).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
