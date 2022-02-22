namespace ItechArt.Survey.Foundation.Authentication.Abstractions;

public enum UserRegistrationErrors
{
    UserNameAlreadyExists,
    EmailAlreadyExists,
    InvalidUserName,
    InvalidEmail,
    InvalidPassword,
    UnknownError
}