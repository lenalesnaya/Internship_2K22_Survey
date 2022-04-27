using System.Collections.Generic;
using System.Threading.Tasks;
using ItechArt.Common;
using ItechArt.Survey.DomainModel.SurveyModel.Answers;
using ItechArt.Survey.DomainModel.SurveyModel.Questions;

namespace ItechArt.Survey.Foundation.SurveyManagement.Abstractions;

public interface ISurveyService
{
    Task<OperationResult<SurveyManagementErrors>> CreateSurveyAsync(DomainModel.SurveyModel.Survey survey);

    Task<OperationResult<SurveyManagementErrors>> EditSurveyAsync(DomainModel.SurveyModel.Survey survey);

    Task<OperationResult<SurveyManagementErrors>> DeleteSurveyAsync(DomainModel.SurveyModel.Survey survey);

    Task<OperationResult<SurveyManagementErrors>> DeleteSurveyByIdAsync(long id);

    Task<DomainModel.SurveyModel.Survey> GetSurveyByIdAsync(long surveyId);

    Task<IList<DomainModel.SurveyModel.Survey>> GetSurveysByTitleAsync(string title);

    Task<IList<DomainModel.SurveyModel.Survey>> GetAllSurveysByUserIdAsync(int userId);
}