using System.ComponentModel.DataAnnotations;

namespace ItechArt.Survey.WebApp.ViewModels;

public class RegistrationViewModel
{
    [StringLength(
        Constants.RegistrationOptionsConstants.UserNameMaxLength,
        MinimumLength = Constants.RegistrationOptionsConstants.UserNameMinLength,
        ErrorMessage = "UserViewModelProfileViewModelProfile name must consist of 3-30 symbols")]
    [RegularExpression(
       Constants.RegistrationOptionsConstants.UserNamePattern,
       ErrorMessage = "Incorrect user name")]
    [Required(ErrorMessage = "UserViewModelProfileViewModelProfile name is required")]
    public string UserName { get; set; }

    [RegularExpression(
        Constants.RegistrationOptionsConstants.EmailPattern,
        ErrorMessage = "Incorrect email address")]
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }

    [StringLength(
        Constants.RegistrationOptionsConstants.PasswordMaxLength,
        MinimumLength = Constants.RegistrationOptionsConstants.PasswordMinLength,
        ErrorMessage = "Password must consist of 8-20 symbols")]
    [RegularExpression(
        Constants.RegistrationOptionsConstants.PasswordPattern,
        ErrorMessage = "Incorrect password")]
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Please, repeat password")]
    [Compare("Password", ErrorMessage = "Password doesn't match, try again !")]
    public string RepeatPassword { get; set; }

    public string Error { get; set; }
}