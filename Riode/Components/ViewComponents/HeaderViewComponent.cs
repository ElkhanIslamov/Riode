using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Riode.Contexts;
using Riode.Models;
using Riode.ViewModels;

namespace Riode.Components.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly RiodeDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public HeaderViewComponent(RiodeDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task <IViewComponentResult> InvokeAsync()
        {
            HeaderViewModel headerViewModel = new HeaderViewModel();
                			
            if(User.Identity.IsAuthenticated) 
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var basketModels = await _context.BasketModels.Where(b => b.AppUserId == user.Id).ToListAsync();
                headerViewModel.BasketModels = basketModels;
                headerViewModel.TotalPrice = basketModels.Sum(b=>b.Product.Price*b.Count);
            }
			
	    	var settings = await _context.Settings.ToDictionaryAsync(x => x.Key, x => x.Value);
            headerViewModel.Settings = settings;

            return View(headerViewModel);
        }
    }
}
