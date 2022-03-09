namespace ItechArt.Survey.Foundation.Validation.Abstractions;

public interface IValidator<TValidationObject, TValidationOptions, TValidationResult>
    where TValidationResult : class
{
    TValidationResult Validate(TValidationObject validationObject, TValidationOptions options);
}