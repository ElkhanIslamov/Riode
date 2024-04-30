using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Riode.Contexts;


namespace Riode.Controllers;

public class HomeController : Controller
{
	private readonly RiodeDbContext _context;

	public HomeController(RiodeDbContext context)
	{
		_context = context;
	}

	public async Task<IActionResult> Index()
	{
		var sliders = await _context.Sliders.ToListAsync();
		var shipping = await _context.Shippings.ToListAsync();
		var product = await _context.Products.ToListAsync();

		HomeViewModel homeViewModel = new HomeViewModel
		{
			Sliders = sliders,
			Shippings = shipping,
			Products = product
		};

		return View(homeViewModel);
	}

}
