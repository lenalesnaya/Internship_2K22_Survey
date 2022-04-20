using ItechArt.Survey.WebApp.ViewModels.SurveyViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ItechArt.Survey.WebApp.Controllers;

public class SurveyController : Controller
{
    [HttpGet]
    public IActionResult CreatingASurvey()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CreatingASurvey(SurveyViewModel surveyViewModel)
    {
        return View();
    }
}