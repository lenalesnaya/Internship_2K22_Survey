using System.Threading.Tasks;
using AutoMapper;
using ItechArt.Survey.DomainModel;
using ItechArt.Survey.Foundation.Authentication.Abstractions;
using ItechArt.Survey.WebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ItechArt.Survey.WebApp.Controllers;

public class AccountController : Controller
{
    private IAuthenticateService _authenticateService;
    private IMapper _mapper;
    private readonly SignInManager<User> _signInManager;


    public AccountController(
        IAuthenticateService authenticateService,
        IMapper mapper,
        SignInManager<User> signInManager)
    {
        _authenticateService = authenticateService;
        _mapper = mapper;
        _signInManager = signInManager;
    }


    [HttpGet]
    public IActionResult Registration()
        => View(new RegistrationViewModel());

    [HttpPost]
    public async Task<IActionResult> Registration(RegistrationViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = _mapper.Map<User>(model);
        var result = await _authenticateService.RegistrationAsync(user, model.Password, model.RepeatPassword);

        if (result.Success)
        {
            await _signInManager.SignInAsync(user, false);
            return RedirectToAction("Profile");
        }

        foreach(var error in result.Errors)
        {
            model.Error = error;
        }

        return View(model);
    }

    [HttpGet]
    [Authorize]
    public IActionResult Profile()
    {
        var model = new ProfileViewModel();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LogOut()
    {
        await _signInManager.SignOutAsync();

        return RedirectToAction("Registration");
    }
}