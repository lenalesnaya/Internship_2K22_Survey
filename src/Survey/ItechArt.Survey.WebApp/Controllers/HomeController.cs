using Microsoft.AspNetCore.Mvc;

namespace ItechArt.Survey.WebApp.Controllers;

public class HomeController : Controller
{
    public IActionResult Home()
    {
        return View();
    }
}