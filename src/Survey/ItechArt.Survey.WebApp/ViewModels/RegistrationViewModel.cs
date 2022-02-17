using System.ComponentModel.DataAnnotations;

namespace ItechArt.Survey.WebApp.ViewModels;

public class RegistrationViewModel
{
    [StringLength(30, MinimumLength = 3, ErrorMessage = "User name must consist of 3-60 symbols")]
    [RegularExpression(
        @"^(?=.{3,30}$)(?![_.0-9])[a-zA-ZА-Яа-я0-9._]+(?<![_.])$",
       ErrorMessage = "Incorrect user name")]
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

    [RegularExpression(
        @"^(?=.*[a-z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,20}$",
        ErrorMessage = "Incorrect password")]
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Please, repeat password")]
    [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
    public string RepeatPassword { get; set; }
}