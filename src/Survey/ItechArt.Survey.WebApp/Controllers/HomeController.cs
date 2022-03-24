using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ItechArt.Survey.WebApp.Controllers;

public class HomeController : Controller
{
    public IActionResult Home()
    {
        return View();
    }
}