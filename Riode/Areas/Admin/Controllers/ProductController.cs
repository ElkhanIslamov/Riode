using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Riode.Areas.Admin.ViewModels.ProductViewModels;
using Riode.Contexts;
using Riode.Models;

namespace Riode.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]

public class ProductController : Controller
{
    private readonly RiodeDbContext _context;

    public ProductController(RiodeDbContext context)
    {
        _context = context;
    }
    [AllowAnonymous]
    public async  Task<IActionResult> Index()
    {
        var products = await _context.Products.Include(p =>p.Category).ToListAsync();
        return View(products);
    }


    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewBag.Categories = await _context.Categories.ToListAsync();   
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(ProductCreateViewModel product)
    {

        Product newProduct = new()
        {
            Name = product.Name,
            Description = product.Description,
            Image = product.Image,
            Price = product.Price,
            Rating = product.Rating,
            CategoryId = product.CategoryId
        };
        await _context.Products.AddAsync(newProduct);

        await _context.SaveChangesAsync();

        return RedirectToAction (nameof(Index));
    }
    public async Task<IActionResult> Detail(int id)
    {
        var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }
  
    [HttpPost]
 
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (product == null)
            return NotFound();
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return Json(new { message = "Your file has been deleted." });
    }
    public async Task<IActionResult> Update(int id)
    {
        var product= await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (product == null)
            return NotFound();
        return View(product);
    }
    [HttpPost]
    public async Task<IActionResult> Update(int id, Product product)
    {
        var dbProduct = await _context.Products.FirstOrDefaultAsync(s => s.Id == id);
        if (dbProduct == null)
            return NotFound();
        dbProduct.Name = product.Name;
        dbProduct.Description = product.Description;
        dbProduct.Price = product.Price;
        dbProduct.Image = product.Image;

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));

    }

}
