using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ItechArt.Survey.DomainModel;
using ItechArt.Survey.Foundation.UserManagement.Abstractions;
using ItechArt.Survey.WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ItechArt.Survey.WebApp.Controllers;

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
            .GetAllUsers())
                .Where(u=>u.Id != -1)
                .Select(dbModel => _mapper.Map<UserViewModel>(dbModel))
                .ToList();

        return View(usersViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await _userService.DeleteUser(id);

        return RedirectToAction("UserManagement", "UserManagement");
    }
}