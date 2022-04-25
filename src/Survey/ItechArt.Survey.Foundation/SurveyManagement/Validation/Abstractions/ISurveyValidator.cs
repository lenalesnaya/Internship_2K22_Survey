using ItechArt.Common.Validation;
using ItechArt.Common.Validation.Abstractions;
using ItechArt.Survey.DomainModel.SurveyModel.Answers;
using ItechArt.Survey.DomainModel.SurveyModel.Questions;
using ItechArt.Survey.Foundation.SurveyManagement.Abstractions;

namespace ItechArt.Survey.Foundation.SurveyManagement.Validation.Abstractions;

public interface ISurveyValidator :
    IValidator<DomainModel.SurveyModel.Survey, SurveyManagementErrors>,
    IValidator<AnswerVariant, SurveyManagementErrors>
{
    ValidationResult<SurveyManagementErrors> ValidateQuestion<TypeOfQuestion>(TypeOfQuestion question)
        where TypeOfQuestion : Question;
}