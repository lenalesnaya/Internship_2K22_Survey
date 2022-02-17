using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using ItechArt.Survey.DomainModel;
using ItechArt.Survey.Foundation.Authentication.Abstractions;
using ItechArt.Survey.Foundation.UserService;
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
    private IUserService _userService;


    public AccountController(
        IAuthenticateService authenticateService,
        IMapper mapper,
        SignInManager<User> signInManager, IUserService userService)
    {
        _authenticateService = authenticateService;
        _mapper = mapper;
        _signInManager = signInManager;
        _userService = userService;
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

        if (!result.Success)
        {
            foreach (var error in result.Errors)
            {
                model.Error = error;
            }

            return View(model);
        }
        await _signInManager.SignInAsync(user, false);

        return RedirectToAction("Profile");
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Profile()
    {
        var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userService.GetCurrent(userId);
        var model = _mapper.Map<ProfileViewModel>(user);

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> LogOut()
    {
        await _signInManager.SignOutAsync();

        return RedirectToAction("Registration");
    }
}