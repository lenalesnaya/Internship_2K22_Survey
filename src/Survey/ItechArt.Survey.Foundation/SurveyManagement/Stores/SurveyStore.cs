using ItechArt.Common;
using ItechArt.Repositories.Abstractions;
using ItechArt.Survey.Foundation.SurveyManagement.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItechArt.Survey.Foundation.SurveyManagement.Stores;

public class SurveyStore
{
    private readonly IUnitOfWork _unitOfWork;


    public SurveyStore(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public static Task<long> GetSurveyIdAsync(DomainModel.SurveyModel.Survey survey)
    {
        return Task.FromResult(survey.Id);
    }

    public static Task<string> GetSurveyTitleAsync(DomainModel.SurveyModel.Survey survey)
    {
        return Task.FromResult(survey.Title);
    }

    public static Task SetSurveyTitleAsync(DomainModel.SurveyModel.Survey survey, string title)
    {
        survey.Title = title;

        return Task.CompletedTask;
    }

    public async Task<OperationResult<SurveyManagementErrors>> CreateAsync(DomainModel.SurveyModel.Survey survey)
    {
        var repository = _unitOfWork.GetRepository<DomainModel.SurveyModel.Survey>();
        survey.DateOfCreation = DateTime.Now;
        survey.DateOfLastUpdating = DateTime.Now;
        repository.Add(survey);
        await _unitOfWork.SaveChangesAsync();

        return OperationResult<SurveyManagementErrors>.CreateSuccessful();
    }

    public async Task<OperationResult<SurveyManagementErrors>> UpdateAsync(DomainModel.SurveyModel.Survey survey)
    {
        var repository = _unitOfWork.GetRepository<DomainModel.SurveyModel.Survey>();
        survey.DateOfLastUpdating = DateTime.Now;
        repository.Update(survey);
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

    public static Task<bool> GetIfSurveyAnonymousAsync(DomainModel.SurveyModel.Survey survey)
    {
        return Task.FromResult(survey.IsAnonymous);
    }

    public static Task SetSurveyAnonymousAsync(DomainModel.SurveyModel.Survey survey, bool isAnonymous)
    {
        survey.IsAnonymous = isAnonymous;

        return Task.CompletedTask;
    }

    public static Task<DateTime> GetDateOfCreationAsync(DomainModel.SurveyModel.Survey survey)
    {
        return Task.FromResult(survey.DateOfCreation);
    }

    public static Task<DateTime> GetDateOfLastUpdatingAsync(DomainModel.SurveyModel.Survey survey)
    {
        return Task.FromResult(survey.DateOfLastUpdating);
    }

    public static Task SetCreatorIdAsync(DomainModel.SurveyModel.Survey survey, int creatorId)
    {
        survey.СreatorId = creatorId;

        return Task.CompletedTask;
    }

    public static Task<int> GetCreatorIdAsync(DomainModel.SurveyModel.Survey survey)
    {
        return Task.FromResult(survey.СreatorId);
    }

    public static Task<string> GetCreatorNameAsync(DomainModel.SurveyModel.Survey survey)
    {
        return Task.FromResult(survey.Сreator.UserName);
    }

    public static Task<int> GetQuantityOfQuestionsAsync(DomainModel.SurveyModel.Survey survey)
    {
        var quantityOfQuestions = survey.AnswerVariantsQuestions.Count + survey.FileAnswerQuestions.Count +
            survey.ScaleAnswerQuestions.Count + survey.StarRatingAnswerQuestions.Count + survey.TextAnswerQuestions.Count;

        return Task.FromResult(quantityOfQuestions);
    }
}