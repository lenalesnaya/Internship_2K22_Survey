using System.ComponentModel.DataAnnotations;

namespace ItechArt.Survey.WebApp.ViewModels;

public class RegistrationViewModel
{
    [StringLength(30, MinimumLength = 3, ErrorMessage = "User name must consist of 3-60 symbols")]
    // [RegularExpression(
    //     @"^([a-zA-Z-а-аЯ-Я])([a-zA-Z-а-аЯ-Я0-9\s])$([a-zA-Z-а-аЯ-Я0-9])", // fix it
    //    ErrorMessage = "Incorrect user name")]
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

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Please, repeat password")]
    public string RepeatPassword { get; set; }
}