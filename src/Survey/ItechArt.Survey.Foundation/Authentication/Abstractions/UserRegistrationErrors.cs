namespace ItechArt.Survey.Foundation.Authentication.Abstractions;

public enum UserRegistrationErrors
{
    UserNameAlreadyExists,
    EmailAlreadyExists,
    UserNameIsRequired,
    IncorrectUserName,
    InvalidUserNameLength,
    EmailIsRequired,
    IncorrectEmail,
    PasswordIsRequired,
    IncorrectPassword,
    InvalidPasswordLength,
    UnknownError
}