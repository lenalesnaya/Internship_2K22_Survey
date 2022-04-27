using System.Text.RegularExpressions;

namespace ItechArt.Survey.Foundation.Authentication.Configuration;

public class RegistrationOptions
{
    public int UserNameMinLength { get; set; }

    public int UserNameMaxLength { get; set; }

    public Regex UserNamePattern { get; set; }

    public Regex EmailPattern { get; set; }

    public int PasswordMinLength { get; set; }

    public int PasswordMaxLength { get; set; }

    public Regex PasswordPattern { get; set; }

    public static string DefaultAvatarFolderPath { get; set; }

    public static string DefaultAvatarFileName { get; set; }
}