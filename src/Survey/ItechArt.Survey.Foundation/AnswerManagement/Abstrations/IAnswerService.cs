using System.Collections.Generic;
using System.Threading.Tasks;
using ItechArt.Common;
using ItechArt.Survey.DomainModel.SurveyModel.Answers;
using ItechArt.Survey.DomainModel.SurveyModel.Questions;
using ItechArt.Survey.Foundation.SurveyManagement.Abstractions;

namespace ItechArt.Survey.Foundation.AnswerManagement.Abstrations;

public interface IAnswerService
{
    Task<OperationResult<SurveyManagementErrors>> CreateAnswerVariantAsync(AnswerVariant answer);

    Task<OperationResult<SurveyManagementErrors>> EditAnswerVariantAsync(AnswerVariant answer);

    Task<OperationResult<SurveyManagementErrors>> DeleteAnswerVariantAsync(AnswerVariant answer);

    Task<AnswerVariant> GetAnswerVariantByIdAsync(long answerId);

    Task<IList<AnswerVariant>> GetAnswerVariantsByQuestionAsync(AnswerVariantsQuestion question);
}