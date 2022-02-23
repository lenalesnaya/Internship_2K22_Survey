using System.Text.RegularExpressions;

namespace ItechArt.Survey.Foundation.Authentication.Configuration;

public class RegistrationOptions
{
    public bool UserNameIsRequired { get; set; }

    public int UserNameMinLength { get; set; }

    public int UserNameMaxLength { get; set; }

    public Regex UserNamePattern { get; set; }

    public bool EmailIsRequired { get; set; }

    public Regex EmailPattern { get; set; }

    public bool PasswordIsRequired { get; set; }

    public int PasswordMinLength { get; set; }

    public int PasswordMaxLength { get; set; }

    public Regex PasswordPattern { get; set; }
}