using ItechArt.Common.Validation;
using ItechArt.Survey.DomainModel.SurveyModel.Answers;
using ItechArt.Survey.DomainModel.SurveyModel.Questions;
using ItechArt.Survey.Foundation.SurveyManagement.Abstractions;
using ItechArt.Survey.Foundation.SurveyManagement.Validation.Abstractions;

namespace ItechArt.Survey.Foundation.SurveyManagement.Validation;

public class SurveyValidator : ISurveyValidator
{
    public ValidationResult<SurveyManagementErrors> Validate(DomainModel.SurveyModel.Survey survey)
    {
        if (survey.Title == null)
        {
            return ValidationResult<SurveyManagementErrors>.CreateUnsuccessful(SurveyManagementErrors.TitleIsRequired);
        }

        if (survey.Title.Length > DomainModel.SurveyModel.Survey.TitleMaxLength)
        {
            return ValidationResult<SurveyManagementErrors>.CreateUnsuccessful(SurveyManagementErrors.InvalidTitleLength);
        }

        return ValidationResult<SurveyManagementErrors>.CreateSuccessful();
    }

    public ValidationResult<SurveyManagementErrors> Validate(AnswerVariant answer)
    {
        if (answer.Text == null)
        {
            return ValidationResult<SurveyManagementErrors>.CreateUnsuccessful(SurveyManagementErrors.AnswerVariantTextIsRequired);
        }

        if (answer.Text.Length > AnswerVariant.TextMaxLength)
        {
            return ValidationResult<SurveyManagementErrors>.CreateUnsuccessful(SurveyManagementErrors.InvalidAnswerVariantTextLength);
        }

        return ValidationResult<SurveyManagementErrors>.CreateSuccessful();
    }


    public ValidationResult<SurveyManagementErrors> ValidateQuestion<TypeOfQuestion>(TypeOfQuestion question)
        where TypeOfQuestion : Question
    {
        var textError = ValidateQuestionText(question.Text);
        if (textError.HasValue)
        {
            return ValidationResult<SurveyManagementErrors>.CreateUnsuccessful(textError.Value);
        }

        if (question is ScaleAnswerQuestion scaleAnswerQuestion)
        {
            if (scaleAnswerQuestion.ScaleMaxValue == 0)
            {
                return ValidationResult<SurveyManagementErrors>.CreateUnsuccessful(SurveyManagementErrors.InvalidScaleMaxValue);
            }
        }

        if (question is StarRatingAnswerQuestion starRatingAnswerQuestion)
        {
            if (starRatingAnswerQuestion.AmountOfStars < StarRatingAnswerQuestion.StarsMinAmount ||
                starRatingAnswerQuestion.AmountOfStars > StarRatingAnswerQuestion.StarsMaxAmount)
            {
                return ValidationResult<SurveyManagementErrors>.CreateUnsuccessful(SurveyManagementErrors.InvalidAmountOfStars);
            }
        }

        return ValidationResult<SurveyManagementErrors>.CreateSuccessful();
    }


    private static SurveyManagementErrors? ValidateQuestionText(string text)
    {
        if (text == null)
        {
            return SurveyManagementErrors.QuestionTextIsRequired;
        }

        if (text.Length > Question.TextMaxLength)
        {
            return SurveyManagementErrors.InvalidQuestionTextLength;
        }

        return null;
    }
}