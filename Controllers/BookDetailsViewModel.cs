using Bookit.Models;
using System.Collections.Generic;

namespace Bookit.Controllers
{
    public class BookDetailsViewModel
    {
        public Book Book { get; set; }
        public IList<Category> Categories { get; set; }
    }
}
