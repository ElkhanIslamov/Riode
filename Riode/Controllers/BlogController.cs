using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Riode.Controllers;

public class BlogController : Controller
{
   

    public async Task <IActionResult>  Index()
    {


        return View();
    }
}
