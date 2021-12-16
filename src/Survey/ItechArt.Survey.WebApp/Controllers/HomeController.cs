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

            return View(GetCounterViewModel(counter.Value));
        }

        [Route("Home/HomePage")]
        [HttpPost]
        public IActionResult IncrementCounter()
        {
            var counter = _counterService.IncrementCounter();

            return View("HomePage", GetCounterViewModel(counter.Value));
        }


        private CounterViewModel GetCounterViewModel(int counterValue)
        {
            var counterViewModel = new CounterViewModel
            {
                Value = counterValue
            };

            return counterViewModel;
        }
    }
}