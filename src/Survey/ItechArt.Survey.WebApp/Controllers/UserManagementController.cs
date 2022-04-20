using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ItechArt.Survey.DomainModel;
using ItechArt.Survey.Foundation.UserManagement.Abstractions;
using ItechArt.Survey.WebApp.ViewModels;
using ItechArt.Survey.WebApp.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ItechArt.Survey.WebApp.Controllers;

[Authorize(Roles = "Administrator")]
public class UserManagementController : Controller
{
    private IUserService _userService;
    private IMapper _mapper;


    public UserManagementController(
        IUserService userService,
        IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    
    [HttpGet]
    public async Task<IActionResult> UserManagement()
    {
        var usersViewModel = (await _userService
            .GetAllUsersAsync())
                .Where(u=>u.Id != -1)
                .Select(dbModel => _mapper.Map<UserViewModel>(dbModel))
                .ToList();

        return View(usersViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var deletionUserResult = await _userService.DeleteUserAsync(id);
        if (!deletionUserResult.IsSuccessful)
        {
            var errorMessage = GetErrorMessage(deletionUserResult.Error.GetValueOrDefault());
            ModelState.AddModelError("", errorMessage);

            return RedirectToAction("UserManagement", "UserManagement");
        }

        return RedirectToAction("UserManagement", "UserManagement");
    }
    
    private static string GetErrorMessage(UserDeletionErrors error)
    {
        var errorMessage = error switch
        {
            UserDeletionErrors.DeletionIsFailed => "Deletion is failed. Something wrong(",
            _ => throw new ArgumentOutOfRangeException(
                $"The value passed as an argument \"{nameof(error)}\" (\"{error}\") is not valid for the method.")
        };

        return errorMessage;
    }
}