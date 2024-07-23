using Microsoft.AspNetCore.Mvc;

namespace Bookit.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
