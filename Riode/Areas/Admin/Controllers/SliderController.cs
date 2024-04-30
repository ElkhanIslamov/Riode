using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Riode.Contexts;
using Riode.Models;

namespace Riode.Areas.Admin.Controllers;

[Area("Admin")]

public class SliderController : Controller
{
    private readonly RiodeDbContext _context;

    public SliderController(RiodeDbContext context)
    {
        _context = context;
    }
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var sliders = await _context.Sliders.AsNoTracking().ToListAsync();

        return View(sliders);
    }
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Slider slider)

    {
        await _context.Sliders.AddAsync(slider);

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Detail(int id)
    {
        var slider = await _context.Sliders.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
        if (slider == null)
        {
            return NotFound();
        }

        return View(slider);
    }
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var slider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
        if (slider == null)
            return NotFound();
        return View(slider);

    }
    [HttpPost]
    [ActionName("Delete")]
    public async Task<IActionResult> DeleteSlide(int id)
    {
        var slider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
        if (slider == null)
            return NotFound();
        _context.Sliders.Remove(slider);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Update(int id)
    {
        var slider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
        if (slider == null)
            return NotFound();
        return View(slider);
    }
    [HttpPost]
    public async Task<IActionResult> Update(int id, Slider slider)
    {
        var dbSlider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
        if (dbSlider == null)
            return NotFound();
        dbSlider.Title = slider.Title;
        dbSlider.Description = slider.Description;
        dbSlider.Offer = slider.Offer;
        dbSlider.Image = slider.Image;

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));

    }
}

