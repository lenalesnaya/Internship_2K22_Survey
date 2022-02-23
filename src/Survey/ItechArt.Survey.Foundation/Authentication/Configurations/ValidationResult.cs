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
            get
            {
                if (!_hasError)
                {
                    throw new InvalidOperationException("Has no error");
                }

                return _error;
            }
        }

        public bool HasError
        {
            get { return _hasError; }
        }


        private ValidationResult()
        {
            _hasError = false;
        }

        private ValidationResult (TError error)
        {
            _error = error;
            _hasError = true;
        }


        public static ValidationResult<TError> CreateResultWithoutError()
        {
            return new ValidationResult<TError>();
        }

        public static ValidationResult<TError> CreateResultWithError(TError error)
        {
            return new ValidationResult<TError>(error);
        }
    }
}