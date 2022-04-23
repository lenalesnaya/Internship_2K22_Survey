using ItechArt.Common;
using ItechArt.Repositories.Abstractions;
using ItechArt.Survey.Foundation.SurveyManagement.Abstractions;
using ItechArt.Survey.Foundation.SurveyManagement.Stores.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ItechArt.Survey.Foundation.SurveyManagement.Stores;

public class SurveyStore : ISurveyStore
{
    private readonly IUnitOfWork _unitOfWork;
    private IHttpContextAccessor _httpContextAccessor;

    public SurveyStore(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
    }


    public async Task<OperationResult<SurveyManagementErrors>> CreateAsync(DomainModel.SurveyModel.Survey survey)
    {
        var repository = _unitOfWork.GetRepository<DomainModel.SurveyModel.Survey>();
        repository.Add(survey);
        await _unitOfWork.SaveChangesAsync();

        return OperationResult<SurveyManagementErrors>.CreateSuccessful();
    }

    public async Task<OperationResult<SurveyManagementErrors>> UpdateAsync(DomainModel.SurveyModel.Survey survey)
    {
        var surveyRepository = _unitOfWork.GetRepository<DomainModel.SurveyModel.Survey>();
        survey.LastUpdateDate = DateTime.Now;
        surveyRepository.Update(survey);
        await _unitOfWork.SaveChangesAsync();

        return OperationResult<SurveyManagementErrors>.CreateSuccessful();
    }

    public async Task<OperationResult<SurveyManagementErrors>> DeleteAsync(DomainModel.SurveyModel.Survey survey)
    {
        var repository = _unitOfWork.GetRepository<DomainModel.SurveyModel.Survey>();
        repository.Remove(survey);
        await _unitOfWork.SaveChangesAsync();

        return OperationResult<SurveyManagementErrors>.CreateSuccessful();
    }

    public async Task<OperationResult<SurveyManagementErrors>> DeleteByIdAsync(long id)
    {
        var repository = _unitOfWork.GetRepository<DomainModel.SurveyModel.Survey>();
        var survey = await FindByIdAsync(id);
        repository.Remove(survey);
        await _unitOfWork.SaveChangesAsync();

        return OperationResult<SurveyManagementErrors>.CreateSuccessful();
    }

    public async Task<DomainModel.SurveyModel.Survey> FindByIdAsync(long surveyId)
    {
        var repository = _unitOfWork.GetRepository<DomainModel.SurveyModel.Survey>();

        return await repository.GetSingleOrDefaultAsync(s => s.Id == surveyId);
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

    public async Task<IList<DomainModel.SurveyModel.Survey>> GetSurveysByUserId(int id)
    {
        var surveyRepository = _unitOfWork.GetRepository<DomainModel.SurveyModel.Survey>();
        var surveys = await surveyRepository.GetWhereAsync(s => s.CreatorId == id);

        return surveys.ToList();
    }
}