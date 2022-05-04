using System.Collections.Generic;
using System.Threading.Tasks;
using ItechArt.Common;
using ItechArt.Survey.DomainModel.SurveyModel.Answers;
using ItechArt.Survey.Foundation.UserAnswerManagement.Abstractions;
using ItechArt.Survey.Foundation.UserAnswerManagement.Stores.Abstractions;

namespace ItechArt.Survey.Foundation.UserAnswerManagement;

public class UserAnswerService : IUserAnswerService
{
    private readonly IUserAnswerStores _userAnswerStores;


    public UserAnswerService(IUserAnswerStores userAnswerStores)
    {
        _userAnswerStores = userAnswerStores;
    }


    public async Task<OperationResult<UserAnswerErrors>> AddAnswers(IList<UserAnswer> answers)
    {
        var result = await _userAnswerStores.AddSelectedAnswer(answers);
        if (!result.IsSuccessful)
        {
            return OperationResult<UserAnswerErrors>.CreateUnsuccessful(UserAnswerErrors.AddAnswerIsFailed);
        }

        return OperationResult<UserAnswerErrors>.CreateSuccessful();
    }
}