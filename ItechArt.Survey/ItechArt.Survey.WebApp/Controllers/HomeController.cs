using ItechArt.Survey.WebApp.Models;
using Microsoft.AspNetCore.Mvc;



namespace ItechArt.Survey.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private static int Counter = 0;
        [HttpGet]
        public IActionResult HomePage()
        {
            var model = new CounterVm();
            model.Counter = Counter;
            return View(model);
        }
        [HttpPost]
        public IActionResult IncrementCounter(CounterVm model)
        {
            Counter++;
            model.Counter = Counter;
            return RedirectToAction("HomePage");
        }
    }
}