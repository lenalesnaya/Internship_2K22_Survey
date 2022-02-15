using System;
using System.Threading.Tasks;
using AutoMapper;
using ItechArt.Survey.DomainModel;
using ItechArt.Survey.Foundation.Authentication.Abstractions;
using ItechArt.Survey.WebApp.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ItechArt.Survey.WebApp.Controllers;

public class AccountController : Controller
{
    private IAuthenticateService _authenticateService;
    private IMapper _mapper;


    public AccountController(
        IAuthenticateService authenticateService,
        IMapper mapper)
    {
        _authenticateService = authenticateService;
        _mapper = mapper;
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
        var result = await _authenticateService.RegistrationAsync(_mapper.Map<User>(model), model.Password);
        
        //await HttpContext.SignInAsync(result.Result);
        return RedirectToAction("Profile");
    }

    [HttpGet]
    [Authorize]
    public IActionResult Profile()
    {
        var model = new ProfileViewModel();
        return View(model);
    }
}