using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Riode.Contexts;

namespace Riode.Controllers
{
    public class ProductDetailController : Controller
    {
        private readonly RiodeDbContext _context;

        public ProductDetailController(RiodeDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

            return View(product);
        }
    }
}
