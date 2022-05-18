using ItechArt.Common;
using ItechArt.Repositories.Abstractions;
using ItechArt.Survey.DomainModel.SurveyModel.Answers;
using ItechArt.Survey.DomainModel.SurveyModel.Questions;
using ItechArt.Survey.Foundation.SurveyManagement.Abstractions;
using ItechArt.Survey.Foundation.SurveyManagement.Stores.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItechArt.Survey.Foundation.SurveyManagement.Stores;

public class AnswerStore : IAnswerStore
{
    private readonly IUnitOfWork _unitOfWork;


    public AnswerStore(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<OperationResult<SurveyManagementErrors>> CreateAsync(AnswerVariant answer)
    {
        var repository = _unitOfWork.GetRepository<AnswerVariant>();

        try
        {
            repository.Add(answer);
            await _unitOfWork.SaveChangesAsync();
        }
        catch
        {
            return OperationResult<SurveyManagementErrors>.CreateUnsuccessful(SurveyManagementErrors.AnswerVariantCreationIsFailed);
        }

        return OperationResult<SurveyManagementErrors>.CreateSuccessful();
    }

    public async Task<OperationResult<SurveyManagementErrors>> UpdateAsync(AnswerVariant answer)
    {
        var survetRepository = _unitOfWork.GetRepository<AnswerVariant>();

        try
        {
            survetRepository.Update(answer);
            await _unitOfWork.SaveChangesAsync();
        }
        catch
        {
            return OperationResult<SurveyManagementErrors>.CreateUnsuccessful(SurveyManagementErrors.AnswerVariantUpdatingIsFailed);
        }

        return OperationResult<SurveyManagementErrors>.CreateSuccessful();
    }

    public async Task<OperationResult<SurveyManagementErrors>> DeleteAsync(AnswerVariant answer)
    {
        var repository = _unitOfWork.GetRepository<AnswerVariant> ();

        try
        {
            repository.Remove(answer);
            await _unitOfWork.SaveChangesAsync();
        }
        catch
        {
            return OperationResult<SurveyManagementErrors>.CreateUnsuccessful(SurveyManagementErrors.AnswerVariantDeletingIsFailed);
        }

        return OperationResult<SurveyManagementErrors>.CreateSuccessful();
    }

    public async Task<AnswerVariant> FindByIdAsync(long answerId)
    {
        var repository = _unitOfWork.GetRepository<AnswerVariant> ();

        return await repository.GetSingleOrDefaultAsync(a => a.Id == answerId);
    }

    public async Task<IList<AnswerVariant>> FindAnswerVariantsByQuestionAsync(AnswerVariantsQuestion question)
    {
        var answersRepository = _unitOfWork.GetRepository<AnswerVariant>();
        var answers = await answersRepository.GetWhereAsync(a => a.QuestionId == question.Id);

        return answers.ToList();
    }
}