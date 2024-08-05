using Bookit.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Bookit.ViewComponents
{
    public class BookListViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;
        public BookListViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _db.Books.Take(4).ToListAsync();
            return View(model);
        }
    }
}
