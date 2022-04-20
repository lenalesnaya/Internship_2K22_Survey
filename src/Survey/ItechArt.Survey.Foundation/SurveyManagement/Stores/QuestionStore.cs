using ItechArt.Common;
using ItechArt.Repositories.Abstractions;
using ItechArt.Survey.DomainModel.SurveyModel.Questions;
using ItechArt.Survey.Foundation.SurveyManagement.Abstractions;
using ItechArt.Survey.Foundation.SurveyManagement.Stores.Abstractions;
using System.Threading.Tasks;

namespace ItechArt.Survey.Foundation.SurveyManagement.Stores;

public class QuestionStore : IQuestionStore
{
    private readonly IUnitOfWork _unitOfWork;


    public QuestionStore(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<OperationResult<SurveyManagementErrors>> CreateAsync<TypeOfQuestion>(TypeOfQuestion question)
        where TypeOfQuestion : Question
    {
        var repository = _unitOfWork.GetRepository<TypeOfQuestion>();
        repository.Add(question);
        await _unitOfWork.SaveChangesAsync();

        return OperationResult<SurveyManagementErrors>.CreateSuccessful();
    }

    public async Task<OperationResult<SurveyManagementErrors>> UpdateAsync<TypeOfQuestion>(TypeOfQuestion question)
        where TypeOfQuestion : Question
    {
        var repository = _unitOfWork.GetRepository<TypeOfQuestion>();
        repository.Update(question);
        await _unitOfWork.SaveChangesAsync();

        return OperationResult<SurveyManagementErrors>.CreateSuccessful();
    }

    public async Task<OperationResult<SurveyManagementErrors>> DeleteAsync<TypeOfQuestion>(TypeOfQuestion question)
        where TypeOfQuestion : Question
    {
        var repository = _unitOfWork.GetRepository<TypeOfQuestion>();
        repository.Remove(question);
        await _unitOfWork.SaveChangesAsync();

        return OperationResult<SurveyManagementErrors>.CreateSuccessful();
    }

    public async Task<TypeOfQuestion> FindByIdAsync<TypeOfQuestion>(long questionId)
        where TypeOfQuestion : Question
    {
        var repository = _unitOfWork.GetRepository<TypeOfQuestion>();

        return await repository.GetSingleOrDefaultAsync(q => q.Id == questionId);
    }
}