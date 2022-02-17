using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;

namespace ItechArt.Survey.Foundation.Authentication.Validation
{
    public class UserValidator : IValidateOptions<UserOptions>
    {
        public ValidateOptionsResult Validate(string name, UserOptions options)
        {
            if (string.IsNullOrEmpty(options.UserName))
            {
                return ValidateOptionsResult.Fail("User name is required");
            }
            else if (options.UserName.Length < 3 || options.UserName.Length > 30)
            {
                return ValidateOptionsResult.Fail("User name must consist of 3-60 symbols");
            }

            var nameRegex = new Regex(@"^(?=.{3,30}$)(?![_.0-9])[a-zA-ZА-Яа-я0-9._]+(?<![_.])$");
            var match = nameRegex.Match(options.UserName);

            if (string.IsNullOrEmpty(match.Value))
            {
                return ValidateOptionsResult.Fail("Incorrect user name");
            }

            if (string.IsNullOrEmpty(options.Email))
            {
                return ValidateOptionsResult.Fail("Email is required");
            }

            var emailRegex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
               @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
               @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            match = emailRegex.Match(options.Email);

            if (string.IsNullOrEmpty(match.Value))
            {
                return ValidateOptionsResult.Fail("Incorrect email address");
            }

            if (string.IsNullOrEmpty(options.Password))
            {
                return ValidateOptionsResult.Fail("Password is required");
            }
            else if (options.Password.Length < 3 || options.Password.Length > 30)
            {
                return ValidateOptionsResult.Fail("Password must consist of 3-60 symbols");
            }

            var passwordRegex = new Regex(@"^(?=.{3,30}$)(?![_.0-9])[a-zA-ZА-Яа-я0-9._]+(?<![_.])$");
            match = passwordRegex.Match(options.Password);

            if (string.IsNullOrEmpty(match.Value))
            {
                return ValidateOptionsResult.Fail("Incorrect password");
            }

            if (string.IsNullOrEmpty(options.RepeatPassword))
            {
                return ValidateOptionsResult.Fail("Please, repeat password");
            }

            if (!string.Equals(options.Password, options.RepeatPassword))
            {
                return ValidateOptionsResult.Fail("Please, repeat password correctly");
            }

            return ValidateOptionsResult.Success;
        }
    }
}