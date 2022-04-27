using ItechArt.Common;
using ItechArt.Repositories.Abstractions;
using ItechArt.Survey.DomainModel.SurveyModel.Questions;
using ItechArt.Survey.Foundation.SurveyManagement.Abstractions;
using ItechArt.Survey.Foundation.SurveyManagement.Stores.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItechArt.Repositories;

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

        try
        {
            repository.Add(question);
            await _unitOfWork.SaveChangesAsync();
        }
        catch
        {
            return OperationResult<SurveyManagementErrors>.CreateUnsuccessful(SurveyManagementErrors.QuestionCreationIsFailed);
        }

        return OperationResult<SurveyManagementErrors>.CreateSuccessful();
    }

    public async Task<OperationResult<SurveyManagementErrors>> UpdateAsync<TypeOfQuestion>(TypeOfQuestion question)
        where TypeOfQuestion : Question
    {
        var repository = _unitOfWork.GetRepository<TypeOfQuestion>();

        try
        {
            repository.Update(question);
            await _unitOfWork.SaveChangesAsync();
        }
        catch
        {
            return OperationResult<SurveyManagementErrors>.CreateUnsuccessful(SurveyManagementErrors.QuestionUpdatingIsFailed);
        }

        return OperationResult<SurveyManagementErrors>.CreateSuccessful();
    }

    public async Task<OperationResult<SurveyManagementErrors>> DeleteAsync<TypeOfQuestion>(TypeOfQuestion question)
        where TypeOfQuestion : Question
    {
        var repository = _unitOfWork.GetRepository<TypeOfQuestion>();

        try
        {
            repository.Remove(question);
            await _unitOfWork.SaveChangesAsync();
        }
        catch
        {
            return OperationResult<SurveyManagementErrors>.CreateUnsuccessful(SurveyManagementErrors.QuestionDeletingIsFailed);
        }

        return OperationResult<SurveyManagementErrors>.CreateSuccessful();
    }

    public async Task<TypeOfQuestion> FindByIdAsync<TypeOfQuestion>(long questionId)
        where TypeOfQuestion : Question
    {
        var repository = _unitOfWork.GetRepository<TypeOfQuestion>();

        return await repository.GetSingleOrDefaultAsync(q => q.Id == questionId);
    }

    public async Task<IList<TypeOfQuestion>> FindOneTypeQuestionsBySurveyIdAsync<TypeOfQuestion>(long surveyId)
        where TypeOfQuestion : Question
    {
        var questionsRepository = _unitOfWork.GetRepository<TypeOfQuestion>();
        var questions = await questionsRepository.GetWhereAsync(
            q => q.SurveyId == surveyId);

        return questions.ToList();
    }
}