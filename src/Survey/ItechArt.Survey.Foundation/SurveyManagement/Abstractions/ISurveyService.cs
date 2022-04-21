using System.Threading.Tasks;
using ItechArt.Common;

namespace ItechArt.Survey.Foundation.SurveyManagement.Abstractions;

public interface ISurveyService
{
    Task<OperationResult<SurveyManagementErrors>> CreateSurvey(string title);
}