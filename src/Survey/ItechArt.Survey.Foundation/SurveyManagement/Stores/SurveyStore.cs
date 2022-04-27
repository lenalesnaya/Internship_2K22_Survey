using ItechArt.Common;
using ItechArt.Repositories.Abstractions;
using ItechArt.Survey.Foundation.SurveyManagement.Abstractions;
using ItechArt.Survey.Foundation.SurveyManagement.Stores.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItechArt.Repositories;

namespace ItechArt.Survey.Foundation.SurveyManagement.Stores;

public class SurveyStore : ISurveyStore
{
    private readonly IUnitOfWork _unitOfWork;


    public SurveyStore(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<OperationResult<SurveyManagementErrors>> CreateAsync(DomainModel.SurveyModel.Survey survey)
    {
        var repository = _unitOfWork.GetRepository<DomainModel.SurveyModel.Survey>();
        try
        {
            repository.Add(survey);
            await _unitOfWork.SaveChangesAsync();
        }
        catch
        {
            return OperationResult<SurveyManagementErrors>.CreateUnsuccessful(SurveyManagementErrors.SurveyCreationIsFailed);
        }

        return OperationResult<SurveyManagementErrors>.CreateSuccessful();
    }

    public async Task<OperationResult<SurveyManagementErrors>> UpdateAsync(DomainModel.SurveyModel.Survey survey)
    {
        var surveyRepository = _unitOfWork.GetRepository<DomainModel.SurveyModel.Survey>();
        survey.LastUpdateDate = DateTime.Now;

        try
        {
            surveyRepository.Update(survey);
            await _unitOfWork.SaveChangesAsync();
        }
        catch
        {
            return OperationResult<SurveyManagementErrors>.CreateUnsuccessful(SurveyManagementErrors.SurveyUpdatingIsFailed);
        }

        return OperationResult<SurveyManagementErrors>.CreateSuccessful();
    }

    public async Task<OperationResult<SurveyManagementErrors>> DeleteAsync(DomainModel.SurveyModel.Survey survey)
    {
        var repository = _unitOfWork.GetRepository<DomainModel.SurveyModel.Survey>();

        try
        {
            repository.Remove(survey);
            await _unitOfWork.SaveChangesAsync();
        }
        catch
        {
            return OperationResult<SurveyManagementErrors>.CreateUnsuccessful(SurveyManagementErrors.SurveyDeletingIsFailed);
        }

        return OperationResult<SurveyManagementErrors>.CreateSuccessful();
    }

    public async Task<OperationResult<SurveyManagementErrors>> DeleteByIdAsync(long surveyId)
    {
        var repository = _unitOfWork.GetRepository<DomainModel.SurveyModel.Survey>();
        var survey = await FindByIdAsync(surveyId);

        try
        {
            repository.Remove(survey);
            await _unitOfWork.SaveChangesAsync();
        }
        catch
        {
            return OperationResult<SurveyManagementErrors>.CreateUnsuccessful(SurveyManagementErrors.SurveyDeletingIsFailed);
        }

        return OperationResult<SurveyManagementErrors>.CreateSuccessful();
    }

    public async Task<DomainModel.SurveyModel.Survey> FindByIdAsync(long surveyId)
    {
        var repository = _unitOfWork.GetRepository<DomainModel.SurveyModel.Survey>();
        var survey = await repository.GetSingleOrDefaultAsync(s => s.Id == surveyId);

        return survey;
    }

    public async Task<IList<DomainModel.SurveyModel.Survey>> FindByTitleAsync(string surveyTitle)
    {
        var repository = _unitOfWork.GetRepository<DomainModel.SurveyModel.Survey>();
        var surveyCollection = await repository.GetWhereAsync(s => s.Title == surveyTitle);

        return surveyCollection.ToList();
    }

    public Task<int> GetQuantityOfQuestionsAsync(DomainModel.SurveyModel.Survey survey)
    {
        var quantityOfQuestions = survey.AnswerVariantsQuestions.Count + survey.FileAnswerQuestions.Count +
            survey.ScaleAnswerQuestions.Count + survey.StarRatingAnswerQuestions.Count + survey.TextAnswerQuestions.Count;

        return Task.FromResult(quantityOfQuestions);
    }

    public async Task<IList<DomainModel.SurveyModel.Survey>> FindSurveysByUserIdAsync(int userId)
    {
        var surveyRepository = _unitOfWork.GetRepository<DomainModel.SurveyModel.Survey>();
        var surveys = await surveyRepository.GetWhereAsync(s => s.CreatorId == userId);

        return surveys.ToList();
    }
}