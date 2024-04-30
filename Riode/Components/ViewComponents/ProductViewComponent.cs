using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Riode.Contexts;

namespace Riode.Components.ViewComponents;

public class ProductViewComponent:ViewComponent
{
    private readonly RiodeDbContext _context;

    public ProductViewComponent(RiodeDbContext context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult>InvokeAsync()
    {
        var products = await _context.Products.Where(p=>!p.IsDeleted).Take(6).ToListAsync();
        return View(products);
    }

}
