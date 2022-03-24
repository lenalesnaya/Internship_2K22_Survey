using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using ItechArt.Survey.DomainModel;
using ItechArt.Survey.Foundation.Authentication.Abstractions;
using ItechArt.Survey.Foundation.UserManagement.Abstractions;
using ItechArt.Survey.WebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ItechArt.Survey.WebApp.Controllers;

public class AccountController : Controller
{
    private readonly IAuthenticateService _authenticateService;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;


    public AccountController(
        IAuthenticateService authenticateService,
        IMapper mapper,
        IUserService userService)
    {
        _authenticateService = authenticateService;
        _mapper = mapper;
        _userService = userService;
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

        var registrationResult = await _authenticateService.RegisterAsync(user, registrationViewModel.Password);
        if (!registrationResult.IsSuccessful)
        {
            ModelState.AddModelError("", GetErrorMessage(registrationResult.Error));

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

    [HttpPost]
    public async Task<IActionResult> LogOut()
    {
        await _authenticateService.SignOutAsync();

        return RedirectToAction("Home", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> CheckIfUserNameExists(string userName)
    {
        var user = await _userService.GetUserByNameAsync(userName);
        var userNameIsClear = user == null;

        return Json(userNameIsClear);
    }

    [HttpGet]
    public async Task<IActionResult> CheckIfEmailExists(string email)
    {
        var user = await _userService.GetUserByEmailAsync(email);
        var emailIsClear = user == null;

        return Json(emailIsClear);
    }


    private static string GetErrorMessage(UserRegistrationErrors? error)
    {
        var errorMessage = error switch
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

        return errorMessage;
    }
}