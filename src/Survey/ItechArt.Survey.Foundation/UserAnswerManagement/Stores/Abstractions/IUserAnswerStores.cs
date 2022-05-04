using System.Collections.Generic;
using System.Threading.Tasks;
using ItechArt.Common;
using ItechArt.Survey.DomainModel.SurveyModel.Answers;
using ItechArt.Survey.Foundation.UserAnswerManagement.Abstractions;

namespace ItechArt.Survey.Foundation.UserAnswerManagement.Stores.Abstractions;

public interface IUserAnswerStores
{
    Task<OperationResult<UserAnswerErrors>> AddSelectedAnswer(IList<UserAnswer> answers);
}