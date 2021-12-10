using ItechArt.Survey.Foundation.CounterServices.Abstractions;
using ItechArt.Survey.WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ItechArt.Survey.WebApp.Controllers
{
    public sealed class HomeController : Controller
    {
        private ICounterService _counterService;


        public HomeController(ICounterService counterService)
        {
            _counterService = counterService;
        }


        [HttpGet]
        public IActionResult HomePage()
        {
            var counter = _counterService.GetCounter();
            var model = new CounterViewModel()
            {
                Value = counter.Value
            };
            
            return View(model);
        }

        [HttpPost]
        public IActionResult IncrementCounter()
        {
            _counterService.IncrementCounter();

            return RedirectToAction("HomePage");
        }
    }
}