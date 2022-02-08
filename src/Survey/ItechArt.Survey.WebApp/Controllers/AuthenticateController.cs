using System;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using ItechArt.Survey.DomainModel;
using ItechArt.Survey.Foundation.Abstractions;
using ItechArt.Survey.WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ItechArt.Survey.WebApp.Controllers;

public class AuthenticateController : Controller
{
    private IAuthenticateService _authenticateService;
    private IMapper _mapper;
    
    public AuthenticateController(
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
        var result = await _authenticateService.RegistrationAsync(_mapper.Map<User>(model), model.Password);
        if (result == HttpStatusCode.BadRequest)
        {
            throw new Exception();
        }
        return View(model);
    }
}