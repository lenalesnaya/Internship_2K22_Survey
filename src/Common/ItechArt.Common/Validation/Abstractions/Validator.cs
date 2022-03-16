using System;

namespace ItechArt.Common.Validation.Abstractions;

public abstract class Validator<TEntity, TError> : IValidator<TEntity, TError>
    where TError: struct, Enum
{
    public ValidationResult<TError> Validate(TEntity entity)
    {
        var error = HandleValidate(entity);

        return error.HasValue
            ? ValidationResult<TError>.CreateUnsuccessful(error.Value)
            : ValidationResult<TError>.CreateSuccessful();
    }


    protected abstract TError? HandleValidate(TEntity entity);
}