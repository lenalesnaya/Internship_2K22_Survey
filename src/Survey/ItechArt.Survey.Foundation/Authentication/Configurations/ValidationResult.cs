using System;

namespace ItechArt.Survey.Foundation.Authentication.Configuration
{
    public class ValidationResult<TError>
        where TError : Enum
    {
        private readonly TError _error;
        private readonly bool _hasError;


        public TError Error
        {
            get => _hasError
                ? _error
                : throw new InvalidOperationException("Has no error");
        }

        public bool HasError => _hasError;


        private ValidationResult (bool hasError, TError error = default)
        {
            _error = error;
            _hasError = hasError;
        }


        public static ValidationResult<TError> CreateResultWithoutError()
        {
            return new ValidationResult<TError>(false);
        }

        public static ValidationResult<TError> CreateResultWithError(TError error)
        {
            return new ValidationResult<TError>(true, error);
        }
    }
}