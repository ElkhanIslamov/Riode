using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Riode.Contexts;

namespace Riode.Components.ViewComponents;

public class FooterViewComponent:ViewComponent
    

{
    private readonly RiodeDbContext _context;

    public FooterViewComponent(RiodeDbContext context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var settings = await _context.Settings.ToDictionaryAsync(x=>x.Key,x=>x.Value);
        return View(settings);
    }

}
