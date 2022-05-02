using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using ItechArt.Survey.DomainModel.UserModel;
using ItechArt.Survey.Foundation.Authentication.Abstractions;
using ItechArt.Survey.Foundation.UserManagement.Abstractions;
using ItechArt.Survey.WebApp.Constants;
using ItechArt.Survey.WebApp.GoogleDriveManagement.Abstractions;
using ItechArt.Survey.WebApp.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ItechArt.Survey.WebApp.Controllers;

public class AccountController : Controller
{
    private readonly IAuthenticateService _authenticateService;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly IGoogleDriveManager _googleDriveManager;


    public AccountController(
        IAuthenticateService authenticateService,
        IMapper mapper,
        IUserService userService,
        IGoogleDriveManager googleDriveManager)
    {
        _authenticateService = authenticateService;
        _mapper = mapper;
        _userService = userService;
        _googleDriveManager = googleDriveManager;
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
            Email = registrationViewModel.Email,
        };

        var registrationResult = await _authenticateService.RegisterAsync(user, registrationViewModel.Password);
        if (!registrationResult.IsSuccessful)
        {
            var errorMessage = GetErrorMessage(registrationResult.Error.GetValueOrDefault());
            ModelState.AddModelError("", errorMessage);

            return View(registrationViewModel);
        }

        return RedirectToAction("Profile");
    }

    [HttpPost]
    public async Task<IActionResult> SetAvatar(IFormFile uploadedFile) // придумать, как удалить файл предыдущ. аватара с гугл диска (если он был)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (uploadedFile != null)
        {
            _googleDriveManager.Authorize();

            using var fileStream = uploadedFile.OpenReadStream();
            byte[] bytes = new byte[uploadedFile.Length];
            fileStream.Read(bytes, 0, (int)uploadedFile.Length);

            _googleDriveManager.FileCreate(userId + uploadedFile.FileName, bytes, out string imageId);

            var avatarSettingResult = await _userService.SetAvatarAsync(userId, imageId);
            if (!avatarSettingResult.IsSuccessful)
            {
                var errorMessage = GetErrorMessage(avatarSettingResult.Error.GetValueOrDefault());
                ModelState.AddModelError("", errorMessage);

                return RedirectToAction("Profile");
            }
        }

        return RedirectToAction("Profile");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteAvatar() // узнать, как удалить файл с гугл диска и добавить сюда вызов этого метода
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var avatarSettingResult = await _userService.DeleteAvatarAsync(userId);
        if (!avatarSettingResult.IsSuccessful)
        {
            var errorMessage = GetErrorMessage(avatarSettingResult.Error.GetValueOrDefault());
            ModelState.AddModelError("", errorMessage);

            return RedirectToAction("Profile");
        }

        return RedirectToAction("Profile");
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Profile()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userService.GetUserByIdAsync(userId);

        byte[] avatarData;
        if (user.AvatarFilePath != null)
            avatarData = GetAvatarData(user.AvatarFilePath);
        else
        {
            avatarData = null;
            user.AvatarFilePath = RegistrationOptionsConstants.DefaultAvatarFilePath;
        }

        var profileViewModel = new ProfileViewModel
        {
            User = _mapper.Map<UserViewModel>(user),
            AvatarData = avatarData
        };

        return View(profileViewModel);
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View(new LoginViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        var authenticationResult = await _authenticateService.AuthenticateAsync(model.UserName, model.Password);
        if (!authenticationResult.IsSuccessful)
        {
            var errorMessage = GetErrorMessage(authenticationResult.Error.GetValueOrDefault());
            ModelState.AddModelError("", errorMessage);

            return View(model);
        }

        return RedirectToAction("Profile");
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


    private byte[] GetAvatarData(string avatarFilePath)
    {
        _googleDriveManager.Authorize();
        var imageBytes = _googleDriveManager.ReadFile(avatarFilePath);

        return imageBytes;
    }

    private static string GetErrorMessage(UserRegistrationErrors error)
    {
        var errorMessage = error switch
        {
            UserRegistrationErrors.UserNameAlreadyExists => "This user name already exists",
            UserRegistrationErrors.EmailAlreadyExists => "This email already exists",
            UserRegistrationErrors.UserNameIsRequired => "User name is required",
            UserRegistrationErrors.InvalidUserNameLength => "Username name must consist of 3-30 symbols",
            UserRegistrationErrors.IncorrectUserName
                => "User name must not begin with a number, or begin/end with a space or an underscore",
            UserRegistrationErrors.EmailIsRequired => "Email is required",
            UserRegistrationErrors.IncorrectEmail => "Incorrect email",
            UserRegistrationErrors.PasswordIsRequired => "Password is required",
            UserRegistrationErrors.InvalidPasswordLength => "Password must consist of 8-20 symbols",
            UserRegistrationErrors.IncorrectPassword
                => "Password must contain at least 1 letter, 1 number and 1 special symbol",
            UserRegistrationErrors.UnknownError => "Unknown error",
            _ => throw new ArgumentOutOfRangeException(
                $"The value passed as an argument \"{nameof(error)}\" ({error}) is not valid for the method.")
        };

        return errorMessage;
    }

    private static string GetErrorMessage(UserAuthenticationErrors error)
    {
        var errorMessage = error switch
        {
            UserAuthenticationErrors.InvalidUsernameOrPassword => "Invalid username or password",
            _ => throw new ArgumentOutOfRangeException(
                $"The value passed as an argument \"{nameof(error)}\" (\"{error}\") is not valid for the method.")
        };

        return errorMessage;
    }

    private static string GetErrorMessage(UserProfileErrors error)
    {
        var errorMessage = error switch
        {
            UserProfileErrors.AvatarSettingIsFailed => "Avatar setting is failed, try later",
            UserProfileErrors.DefaultAvatarSettingIsFailed => "Avatar deleting is failed, try later",
            _ => throw new ArgumentOutOfRangeException(
                $"The value passed as an argument \"{nameof(error)}\" (\"{error}\") is not valid for the method.")
        };

        return errorMessage;
    }
}