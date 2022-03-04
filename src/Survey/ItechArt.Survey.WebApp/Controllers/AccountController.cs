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
    private readonly SignInManager<User> _signInManager;
    private readonly IAuthenticateService _authenticateService;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;


    public AccountController(
        IAuthenticateService authenticateService,
        IMapper mapper,
        SignInManager<User> signInManager, IUserService userService)
    {
        _signInManager = signInManager;
        _authenticateService = authenticateService;
        _mapper = mapper;
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
        var result = await _authenticateService.RegisterAsync(user, model.Password);

        if (!result.Success)
        {
            model.Error = result.Error switch
            {
                UserRegistrationErrors.UserNameAlreadyExists => "This user name already exists",
                UserRegistrationErrors.EmailAlreadyExists => "This email already exists",
                UserRegistrationErrors.UserNameIsRequired => "User name is required",
                UserRegistrationErrors.InvalidUserNameLength => "User name must consist of 3-30 symbols",
                UserRegistrationErrors.IncorrectUserName => "Incorrect user name",
                UserRegistrationErrors.EmailIsRequired => "Email is required",
                UserRegistrationErrors.IncorrectEmail => "Incorrect email",
                UserRegistrationErrors.PasswordIsRequired => "Password is required",
                UserRegistrationErrors.InvalidPasswordLength => "Password must consist of 8-20 symbols",
                UserRegistrationErrors.IncorrectPassword => "Incorrect password",
                _ => "Unknown error"
            };

            return View(model);
        }

        return RedirectToAction("Profile");
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Profile()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userService.GetUserByIdAsync(userId);
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