using System.Threading.Tasks;
using ItechArt.Common;
using ItechArt.Common.Logging.Abstractions;
using ItechArt.Common.Logging.Extensions;
using ItechArt.Survey.Foundation.SurveyManagement.Abstractions;
using ItechArt.Survey.Foundation.SurveyManagement.Stores;
using ItechArt.Survey.Foundation.SurveyManagement.Stores.Abstractions;

namespace ItechArt.Survey.Foundation.SurveyManagement;

public class SurveyService : ISurveyService
{
    private readonly ISurveyStore _surveyStore;
    private readonly ILogger _logger;

    public SurveyService(ISurveyStore surveyStore, ILogger logger)
    {
        _surveyStore = surveyStore;
        _logger = logger;
    }

    public async Task<OperationResult<SurveyManagementErrors>> CreateSurvey(string title)
    {
        var creatingResult = await _surveyStore.CreateAsync();
        if (!creatingResult.IsSuccessful)
        {
            _logger.LogWarning($"Creating is failed: {creatingResult.Error}.");

            return OperationResult<SurveyManagementErrors>.CreateUnsuccessful(SurveyManagementErrors.CreationIsFailed);
        }
        return OperationResult<SurveyManagementErrors>.CreateSuccessful();
    }
}