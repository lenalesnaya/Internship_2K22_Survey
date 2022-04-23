using System.Collections.Generic;
using System.Threading.Tasks;
using ItechArt.Common;

namespace ItechArt.Survey.Foundation.SurveyManagement.Abstractions;

public interface ISurveyService
{
    Task<OperationResult<SurveyManagementErrors>> CreateSurvey(DomainModel.SurveyModel.Survey survey);

    Task<DomainModel.SurveyModel.Survey> GetSurveyById(long id);

    Task<IList<DomainModel.SurveyModel.Survey>> GetAllSurveyByUserId(int id);

    Task<OperationResult<SurveyManagementErrors>> DeleteSurveyById(long id);
}