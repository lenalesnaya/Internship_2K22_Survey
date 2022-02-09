using System.ComponentModel.DataAnnotations;

namespace ItechArt.Survey.WebApp.ViewModels;

public class RegistrationViewModel
{
    [Range(3, 20)]

    [Required(ErrorMessage = "User name is required")]
    public string UserName { get; set; }

    [EmailAddress]
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }

    [Required(ErrorMessage = "This field is required")]
    public string RepeatPassword { get; set; }
}