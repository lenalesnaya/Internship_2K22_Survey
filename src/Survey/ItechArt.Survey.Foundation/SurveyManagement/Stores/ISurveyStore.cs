using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ItechArt.Common;
using ItechArt.Survey.Foundation.SurveyManagement.Abstractions;

namespace ItechArt.Survey.Foundation.SurveyManagement.Stores;

public interface ISurveyStore
{
    Task<long> GetSurveyIdAsync(DomainModel.SurveyModel.Survey survey);

    Task<string> GetSurveyTitleAsync(DomainModel.SurveyModel.Survey survey);

    Task SetSurveyTitleAsync(DomainModel.SurveyModel.Survey survey, string title);

    Task<OperationResult<SurveyManagementErrors>> CreateAsync(DomainModel.SurveyModel.Survey survey);

    Task<OperationResult<SurveyManagementErrors>> UpdateAsync(DomainModel.SurveyModel.Survey survey);

    Task<OperationResult<SurveyManagementErrors>> DeleteAsync(DomainModel.SurveyModel.Survey survey);

    Task<DomainModel.SurveyModel.Survey> FindByIdAsync(long surveyId);

    Task<IList<DomainModel.SurveyModel.Survey>> FindByTitleAsync(string surveyTitle);

    Task<bool> GetIfSurveyAnonymousAsync(DomainModel.SurveyModel.Survey survey);

    Task SetSurveyAnonymousAsync(DomainModel.SurveyModel.Survey survey, bool isAnonymous);

    Task<DateTime> GetDateOfCreationAsync(DomainModel.SurveyModel.Survey survey);

    Task SetCreatorIdAsync(DomainModel.SurveyModel.Survey survey, int creatorId);

    Task<int> GetCreatorIdAsync(DomainModel.SurveyModel.Survey survey);

    Task<string> GetCreatorNameAsync(DomainModel.SurveyModel.Survey survey);

    Task<int> GetQuantityOfQuestionsAsync(DomainModel.SurveyModel.Survey survey);
}