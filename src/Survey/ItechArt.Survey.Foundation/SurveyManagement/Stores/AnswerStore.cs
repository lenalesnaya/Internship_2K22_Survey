using ItechArt.Common;
using ItechArt.Repositories.Abstractions;
using ItechArt.Survey.DomainModel.SurveyModel.Answers;
using ItechArt.Survey.Foundation.SurveyManagement.Abstractions;
using System.Threading.Tasks;

namespace ItechArt.Survey.Foundation.SurveyManagement.Stores;

public class AnswerStore
{
    private readonly IUnitOfWork _unitOfWork;


    public AnswerStore(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<OperationResult<SurveyManagementErrors>> CreateAsync(AnswerVariant answer)
    {
        var repository = _unitOfWork.GetRepository<AnswerVariant>();
        repository.Add(answer);
        await _unitOfWork.SaveChangesAsync();

        return OperationResult<SurveyManagementErrors>.CreateSuccessful();
    }

    public async Task<OperationResult<SurveyManagementErrors>> UpdateAsync(AnswerVariant answer)
    {
        var survetRepository = _unitOfWork.GetRepository<AnswerVariant>();
        survetRepository.Update(answer);
        await _unitOfWork.SaveChangesAsync();

        return OperationResult<SurveyManagementErrors>.CreateSuccessful();
    }

    public async Task<OperationResult<SurveyManagementErrors>> DeleteAsync(AnswerVariant answer)
    {
        var repository = _unitOfWork.GetRepository<AnswerVariant> ();
        repository.Remove(answer);
        await _unitOfWork.SaveChangesAsync();

        return OperationResult<SurveyManagementErrors>.CreateSuccessful();
    }

    public async Task<AnswerVariant> FindByIdAsync(long answerId)
    {
        var repository = _unitOfWork.GetRepository<AnswerVariant> ();

        return await repository.GetSingleOrDefaultAsync(a => a.Id == answerId);
    }
}