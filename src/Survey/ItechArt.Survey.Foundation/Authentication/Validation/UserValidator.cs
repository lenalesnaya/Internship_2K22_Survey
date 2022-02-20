using ItechArt.Common;
using ItechArt.Survey.DomainModel;
using ItechArt.Survey.Foundation.Authentication.Abstractions;
using System.Text.RegularExpressions;

namespace ItechArt.Survey.Foundation.Authentication.Validation;

public class UserValidator
{
    public static OperationResult<UserRegistrationErrors> Validate(User user, string password)
    {
        if (string.IsNullOrEmpty(user.UserName))
        {
            return OperationResult<UserRegistrationErrors>.GetFailureResult(
                UserRegistrationErrors.ValidationError,
                "User name is required");
        }
        else if (user.UserName.Length < 3 || user.UserName.Length > 30)
        {
            return OperationResult<UserRegistrationErrors>.GetFailureResult(
                UserRegistrationErrors.ValidationError,
                "User name must consist of 3-30 symbols");
        }

        var regex = new Regex(@"^(?=.{3,30}$)(?![_.0-9])[a-zA-ZА-Яа-я0-9._]+(?<![_.])$");
        var match = regex.Match(user.UserName);

        if (string.IsNullOrEmpty(match.Value))
        {
            return OperationResult<UserRegistrationErrors>.GetFailureResult(
                UserRegistrationErrors.ValidationError,
                "Incorrect user name");
        }

        if (string.IsNullOrEmpty(user.Email))
        {
            return OperationResult<UserRegistrationErrors>.GetFailureResult(
                UserRegistrationErrors.ValidationError,
                "Email is required");
        }

        regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
           @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
           @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        match = regex.Match(user.Email);

        if (string.IsNullOrEmpty(match.Value))
        {
            return OperationResult<UserRegistrationErrors>.GetFailureResult(
                UserRegistrationErrors.ValidationError,
                "Incorrect email address");
        }

        if (string.IsNullOrEmpty(password))
        {
            return OperationResult<UserRegistrationErrors>.GetFailureResult(
                UserRegistrationErrors.ValidationError,
                "Password is required");
        }
        else if (password.Length < 8 || password.Length > 20)
        {
            return OperationResult<UserRegistrationErrors>.GetFailureResult(
                UserRegistrationErrors.ValidationError,
                "Password must consist of 8-20 symbols");
        }

        regex = new Regex(@"^(?=.*[a-z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,20}$");
        match = regex.Match(password);

        if (string.IsNullOrEmpty(match.Value))
        {
            return OperationResult<UserRegistrationErrors>.GetFailureResult(
                UserRegistrationErrors.ValidationError,
                "Incorrect password");
        }

        return OperationResult<UserRegistrationErrors>.GetSuccessfulResult();
    }
}