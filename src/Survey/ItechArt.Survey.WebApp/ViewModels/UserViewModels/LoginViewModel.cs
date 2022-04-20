using System.ComponentModel.DataAnnotations;

namespace ItechArt.Survey.WebApp.ViewModels.UserViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Please, enter email")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Please, enter password")]
    public string Password { get; set; }
}