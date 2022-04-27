using System.Collections.Generic;
using System.Threading.Tasks;
using ItechArt.Common;
using ItechArt.Survey.DomainModel.SurveyModel.Answers;
using ItechArt.Survey.DomainModel.SurveyModel.Questions;
using ItechArt.Survey.Foundation.SurveyManagement.Abstractions;

namespace ItechArt.Survey.Foundation.SurveyManagement.Stores.Abstractions;

public interface IAnswerStore
{
    Task<OperationResult<SurveyManagementErrors>> CreateAsync(AnswerVariant answer);

    Task<OperationResult<SurveyManagementErrors>> UpdateAsync(AnswerVariant answer);

    Task<OperationResult<SurveyManagementErrors>> DeleteAsync(AnswerVariant answer);

    Task<AnswerVariant> FindByIdAsync(long answerId);

    Task<IList<AnswerVariant>> FindAnswerVariantsByQuestionAsync(AnswerVariantsQuestion question);
}