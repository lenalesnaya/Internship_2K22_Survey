namespace ItechArt.Survey.WebApp.Constants;

public static class RegistrationOptionsConstants
{
    public const int UserNameMinLength = 3;
    public const int UserNameMaxLength = 30;
    public const string UserNamePattern = @"^(?![_0-9\s])[a-zA-ZА-Яа-я0-9\s_]+(?<![_\s])$";
    public const string EmailPattern =
        @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
        @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
        @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
    public const int PasswordMinLength = 8;
    public const int PasswordMaxLength = 20;
    public const string PasswordPattern = @"^(?=.*[a-z])(?=.*\d)(?=.*[^\da-zA-Z])\S{0,}$";
    public const string DefaultAvatarFilePath = "/images/DefaultAvatar.png";
}