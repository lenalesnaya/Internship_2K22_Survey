using System;

namespace ItechArt.Common.Validation;

public class ValidationResult<TError>
    where TError : struct, Enum
{
    public bool IsSuccessful { get; }

    public TError? Error { get; }


    private ValidationResult (bool isSuccessful, TError? error)
    {
        IsSuccessful = isSuccessful;
        Error = error;
    }


    public static ValidationResult<TError> CreateSuccessful()
    {
        return new ValidationResult<TError>(true, null);
    }

    public static ValidationResult<TError> CreateUnsuccessful(TError error)
    {
        return new ValidationResult<TError>(false, error);
    }
}