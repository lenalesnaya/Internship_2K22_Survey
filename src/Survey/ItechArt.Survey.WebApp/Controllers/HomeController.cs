using System.Threading.Tasks;
using ItechArt.Survey.DomainModel;
using ItechArt.Survey.Foundation.Counters.Abstractions;
using ItechArt.Survey.WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ItechArt.Survey.WebApp.Controllers;

public class HomeController : Controller
{
    private readonly ICounterService _counterService;


    public HomeController(ICounterService counterService)
    {
        _counterService = counterService;
    }


    [HttpGet]
    public async Task<IActionResult> HomePage()
    {
        var counter = await _counterService.GetCounterAsync();
        var counterViewModel = GetCounterViewModel(counter);

        return View(counterViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> IncrementCounter()
    {
        var counter = await _counterService.IncrementCounterAsync();
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