using System;

namespace ItechArt.Common.Validation.Abstractions;

public abstract class Validator<TEntity, TError> : IValidator<TEntity, TError>
    where TError: struct, Enum
{
    public ValidationResult<TError> Validate(TEntity entity)
    {
        var error = ValidateWithErrorReturning(entity);

        return error.HasValue
            ? ValidationResult<TError>.CreateUnsuccessful(error.Value)
            : ValidationResult<TError>.CreateSuccessful();
    }


    protected abstract TError? ValidateWithErrorReturning(TEntity entity);
}