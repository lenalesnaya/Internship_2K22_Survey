using Microsoft.AspNetCore.Mvc;

namespace ItechArt.Survey.WebApp.Controllers;

public class SurveyController : Controller
{
    public IActionResult CreatingASurvey()
    {
        return View();
    }
}