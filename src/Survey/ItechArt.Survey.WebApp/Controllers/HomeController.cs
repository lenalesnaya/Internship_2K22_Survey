using ItechArt.Survey.Foundation.Counters.Abstractions;
using ItechArt.Survey.WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ItechArt.Survey.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICounterService _counterService;


        public HomeController(ICounterService counterService)
        {
            _counterService = counterService;
        }


        [HttpGet]
        public IActionResult HomePage()
        {
            var counter = _counterService.GetCounter();
            var counterViewModel = new CounterViewModel
            {
                Value = counter.Value
            };

            return View(counterViewModel);
        }

        [HttpPost]
        public IActionResult IncrementCounter()
        {
            var counter = _counterService.IncrementCounter();
            var counterViewModel = new CounterViewModel
            {
                Value = counter.Value
            };

            return View("HomePage", counterViewModel);
        }
    }
}