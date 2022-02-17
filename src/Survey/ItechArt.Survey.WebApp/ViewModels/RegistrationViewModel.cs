using System.ComponentModel.DataAnnotations;

namespace ItechArt.Survey.WebApp.ViewModels;

public class RegistrationViewModel
{
    [StringLength(30, MinimumLength = 3, ErrorMessage = "User name must consist of 3-30 symbols")]
    // добавить регулярку
    [Required(ErrorMessage = "User name is required")]
    public string UserName { get; set; }

    [EmailAddress(ErrorMessage = "Incorrect email address")]
    [RegularExpression(
        @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
        @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
        @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
        ErrorMessage = "Incorrect email address")]
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }

    [StringLength(20, MinimumLength = 8, ErrorMessage = "Password must consist of 8-20 symbols")]
    // добавить регулярку
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Please, repeat password")]
    public string RepeatPassword { get; set; }

    public string Error { get; set; }
}