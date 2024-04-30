using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Riode.Contexts;

namespace Riode.Areas.Admin.Controllers;

[Area("Admin")]
public class CategoryController : Controller
{
    private readonly RiodeDbContext _context;
   
    public CategoryController(RiodeDbContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Index()
    {
       
        return View();
    }
}
