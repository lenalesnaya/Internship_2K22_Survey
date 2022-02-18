namespace ItechArt.Survey.Foundation.Authentication.Validation;

public class UserOptions
{
    public const string optionsName = "UserOptions";


    public string UserName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string RepeatPassword { get; set; }
}