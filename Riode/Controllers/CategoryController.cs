using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg;
using Riode.Contexts;
using Riode.Models;

namespace Riode.Controllers;

    public class CategoryController : Controller
    {
	private readonly RiodeDbContext _context;
	private readonly UserManager<AppUser> _userManager;


    public CategoryController(RiodeDbContext context, UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    public async Task<IActionResult> Index()
	{
		int productCount = await _context.Products.Where(p => !p.IsDeleted).CountAsync();
		ViewBag.ProductCount = productCount;

		return View();
	}

	public async Task<IActionResult> LoadMore(int skip)
	{
		int productCount = await _context.Products.Where(p => !p.IsDeleted).CountAsync();
		if (skip >= productCount)
			return BadRequest();


		List<Product> products = await _context.Products.Where(p => !p.IsDeleted)
			.Skip(skip).Take(6).ToListAsync();

		return PartialView("_ProductPartial", products);
	}
	public async Task<IActionResult> ProductDetail(int id)
	{
		var product = await _context.Products.FirstOrDefaultAsync(p=> p.Id == id && !p.IsDeleted) ;

		return PartialView("_ProductModalPartial", product );
	}
    

    [Authorize]
	public async Task<IActionResult>AddProductToBasket(int productId )
	{
		var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId && !p.IsDeleted);
		if (product == null)
			return NotFound();
		var user = await _userManager.FindByNameAsync(User.Identity.Name);

		var basketModel = await _context.BasketModels.Include(b=>b.Product)
			.FirstOrDefaultAsync(b => b.ProductId == productId && b.AppUserId==user.Id);
		if (basketModel == null)
		{

			BasketModel newBasketModel = new BasketModel()
			{
				ProductId = productId,
				AppUserId = user.Id,
				Count = 1,
				CreatedDate = DateTime.UtcNow,

			};
			await _context.BasketModels.AddAsync(newBasketModel);
		}
		else
			basketModel.Count++;
		
		await _context.SaveChangesAsync();


       return RedirectToAction("Index");
	}

}
