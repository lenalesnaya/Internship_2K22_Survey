using ItechArt.Survey.Foundation.CounterServices.Abstractions;
using ItechArt.Survey.WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ItechArt.Survey.WebApp.Controllers
{
    public sealed class HomeController : Controller
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
            var counterValue = new CounterViewModel
            {
                Value = counter.Value
            };

            return View(counterValue);
        }

        [HttpPost]
        public IActionResult IncrementCounter()
        {
            _counterService.IncrementCounter();

            return RedirectToAction("HomePage");
        }
    }
}