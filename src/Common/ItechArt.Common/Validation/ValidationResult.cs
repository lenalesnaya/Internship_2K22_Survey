using System;

namespace ItechArt.Common.Validation;

public class ValidationResult<TError>
    where TError : Enum
{
    private readonly TError _error;


    public bool Success { get; }

    public TError Error
        => !Success
            ? _error
            : throw new InvalidOperationException("Successful result has no error");


    private ValidationResult (bool success, TError error)
    {
        Success = success;
        _error = error;
    }


    public static ValidationResult<TError> CreateSuccessfulResult()
    {
        return new ValidationResult<TError>(true, default);
    }

    public static ValidationResult<TError> CreateFailureResult(TError error)
    {
        return new ValidationResult<TError>(false, error);
    }
}