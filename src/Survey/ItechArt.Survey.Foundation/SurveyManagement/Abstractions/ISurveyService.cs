using System.Collections.Generic;
using System.Threading.Tasks;
using ItechArt.Common;

namespace ItechArt.Survey.Foundation.SurveyManagement.Abstractions;

public interface ISurveyService
{
    Task<OperationResult<SurveyManagementErrors>> CreateSurvey(string title);

    Task<IList<DomainModel.SurveyModel.Survey>> GetAllSurveyByUserId(int id);
}