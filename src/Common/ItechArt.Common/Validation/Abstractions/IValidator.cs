using System;

namespace ItechArt.Common.Validation.Abstractions;

public interface IValidator<TEntity, TError>
    where TError : Enum
{
    ValidationResult<TError> Validate(TEntity entity);
}