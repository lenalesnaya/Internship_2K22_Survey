using System.Collections.Generic;
using System.Threading.Tasks;
using ItechArt.Common;
using ItechArt.Survey.DomainModel.SurveyModel.Answers;

namespace ItechArt.Survey.Foundation.UserAnswerManagement.Abstractions;

public interface IUserAnswerService
{
    Task<OperationResult<UserAnswerErrors>> AddAnswers(IList<UserAnswer> answers);
}