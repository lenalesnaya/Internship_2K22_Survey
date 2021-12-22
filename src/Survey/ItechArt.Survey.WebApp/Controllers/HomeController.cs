using ItechArt.Survey.DomainModel;
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
            var counterViewModel = GetCounterViewModel(counter);

            return View(counterViewModel);
        }

        [HttpPost]
        public IActionResult IncrementCounter()
        {
            var counter = _counterService.IncrementCounter();
            var counterViewModel = GetCounterViewModel(counter);

            return View("HomePage", counterViewModel);
        }


        private static CounterViewModel GetCounterViewModel(Counter counter)
        {
            var counterViewModel = new CounterViewModel
            {
                Value = counter.Value
            };

            return counterViewModel;
        }
    }
}