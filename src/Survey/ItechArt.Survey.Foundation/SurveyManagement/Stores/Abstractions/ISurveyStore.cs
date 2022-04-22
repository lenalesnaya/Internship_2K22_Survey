using System.Collections.Generic;
using System.Threading.Tasks;
using ItechArt.Common;
using ItechArt.Survey.Foundation.SurveyManagement.Abstractions;

namespace ItechArt.Survey.Foundation.SurveyManagement.Stores.Abstractions;

public interface ISurveyStore
{
    Task<OperationResult<SurveyManagementErrors>> CreateAsync(string title);

    Task<OperationResult<SurveyManagementErrors>> UpdateAsync(DomainModel.SurveyModel.Survey survey);

    Task<OperationResult<SurveyManagementErrors>> DeleteAsync(DomainModel.SurveyModel.Survey survey);

    Task<DomainModel.SurveyModel.Survey> FindByIdAsync(long surveyId);

    Task<IList<DomainModel.SurveyModel.Survey>> FindByTitleAsync(string surveyTitle);

    Task<int> GetQuantityOfQuestionsAsync(DomainModel.SurveyModel.Survey survey);

    Task<IList<DomainModel.SurveyModel.Survey>> GetSurveysByUserId(int id);
}