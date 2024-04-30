using Microsoft.AspNetCore.Mvc;

namespace Riode.Areas.Admin.Controllers;

[Area("Admin")]

public class BlogController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
