using System.Collections.Generic;
using System.Threading.Tasks;
using ItechArt.Common;
using ItechArt.Repositories.Abstractions;
using ItechArt.Survey.DomainModel.SurveyModel.Answers;
using ItechArt.Survey.Foundation.UserAnswerManagement.Abstractions;
using ItechArt.Survey.Foundation.UserAnswerManagement.Stores.Abstractions;

namespace ItechArt.Survey.Foundation.UserAnswerManagement.Stores;

public class UserAnswerStore : IUserAnswerStores
{
    private IUnitOfWork _unitOfWork;


    public UserAnswerStore(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<OperationResult<UserAnswerErrors>> AddSelectedAnswer(IList<UserAnswer> answers)
    {
        var answerRepository = _unitOfWork.GetRepository<UserAnswer>();
        try
        {
            foreach (var answer in answers)
            {
                answerRepository.Add(answer);
            }
            await _unitOfWork.SaveChangesAsync();

            return OperationResult<UserAnswerErrors>.CreateSuccessful();
        }
        catch
        {
            return OperationResult<UserAnswerErrors>.CreateUnsuccessful(UserAnswerErrors.AddAnswerIsFailed);
        }
    }
}