using System.ComponentModel.DataAnnotations;

namespace ItechArt.Survey.WebApp.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Please enter email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Please enter password")]
    public string Password { get; set; }
}