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
    private readonly UserManager<User> _userManager;


    public AccountController(
        IAuthenticateService authenticateService,
        IMapper mapper,
        SignInManager<User> signInManager,
        IUserService userService,
        UserManager<User> userManager)
    {
        _signInManager = signInManager;
        _authenticateService = authenticateService;
        _mapper = mapper;
        _userService = userService;
        _userManager = userManager;
    }


    [HttpGet]
    public IActionResult Registration()
        => View(new RegistrationViewModel());

    [HttpPost]
    public async Task<IActionResult> Registration(RegistrationViewModel registrationViewModel)
    {
        var user = new User
        {
            UserName = registrationViewModel.UserName,
            Email = registrationViewModel.Email
        };

        var result = await _authenticateService.RegisterAsync(user, registrationViewModel.Password);

        if (!result.IsSuccessful)
        {
            ModelState.AddModelError("", GetErrorMessage(result.Error));

            return View(registrationViewModel);
        }

        return RedirectToAction("Profile");
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Profile()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userService.GetUserByIdAsync(userId);
        var profileViewModel = new ProfileViewModel
        {
            User = _mapper.Map<UserViewModel>(user)
        };

        return View(profileViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> LogOut()
    {
        await _signInManager.SignOutAsync();

        return RedirectToAction("Registration");
    }

    [HttpPost]
    public async Task<IActionResult> IsUserNameExists(string userName)
    {
        var userNameExists = await _userManager.FindByNameAsync(userName);

        return userNameExists == null
            ? Json(true)
            : Json(false);
    }

    [HttpPost]
    public async Task<IActionResult> IsEmailExists(string email)
    {
        var userEmailExists = await _userManager.FindByEmailAsync(email);

        return userEmailExists == null
            ? Json(true)
            : Json(false);
    }


    private static string GetErrorMessage(UserRegistrationErrors? error)
    {
        var message = error switch
        {
            UserRegistrationErrors.UserNameAlreadyExists => "This user name already exists",
            UserRegistrationErrors.EmailAlreadyExists => "This email already exists",
            UserRegistrationErrors.UserNameIsRequired => "Username name is required",
            UserRegistrationErrors.InvalidUserNameLength => "Username name must consist of 3-30 symbols",
            UserRegistrationErrors.IncorrectUserName => "Incorrect user name",
            UserRegistrationErrors.EmailIsRequired => "Email is required",
            UserRegistrationErrors.IncorrectEmail => "Incorrect email",
            UserRegistrationErrors.PasswordIsRequired => "Password is required",
            UserRegistrationErrors.InvalidPasswordLength => "Password must consist of 8-20 symbols",
            UserRegistrationErrors.IncorrectPassword => "Incorrect password",
            _ => "Unknown error"
        };

        return message;
    }
}