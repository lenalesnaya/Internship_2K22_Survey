using ItechArt.Survey.Foundation.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;

namespace ItechArt.Survey.WebApp.Controllers
{
    public sealed class HomeController : Controller
    {
        private static int _counter;
        private ICounterService _counterService;


        public HomeController(ICounterService counterService)
        {
            _counterService = counterService;
        }


        [HttpGet]
        public IActionResult HomePage()
            => View(_counterService.CounterStatus(_counter));

        [HttpPost]
        public IActionResult IncrementCounter()
        {
            _counterService.IncrementCounter(ref _counter);

            return RedirectToAction("HomePage");
        }
    }
}