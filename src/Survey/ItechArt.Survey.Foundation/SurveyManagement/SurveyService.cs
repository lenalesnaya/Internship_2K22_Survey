using System.Collections.Generic;
using System.Threading.Tasks;
using ItechArt.Common;
using ItechArt.Common.Logging.Abstractions;
using ItechArt.Common.Logging.Extensions;
using ItechArt.Survey.DomainModel.SurveyModel.Answers;
using ItechArt.Survey.DomainModel.SurveyModel.Questions;
using ItechArt.Survey.Foundation.QuestionManagement.Abstractions;
using ItechArt.Survey.Foundation.SurveyManagement.Abstractions;
using ItechArt.Survey.Foundation.SurveyManagement.Stores.Abstractions;
using ItechArt.Survey.Foundation.SurveyManagement.Validation.Abstractions;

namespace ItechArt.Survey.Foundation.SurveyManagement;

public class SurveyService : ISurveyService
{
    private readonly ISurveyStore _surveyStore;
    private readonly ILogger _logger;
    private readonly ISurveyValidator _surveyValidator;
    private readonly IQuestionService _questionService;

    public SurveyService(
        ISurveyStore surveyStore,
        ILogger logger,
        ISurveyValidator surveyValidator,
        IQuestionService questionService)
    {
        _surveyStore = surveyStore;
        _logger = logger;
        _surveyValidator = surveyValidator;
        _questionService = questionService;
    }

    public async Task<OperationResult<SurveyManagementErrors>> CreateSurveyAsync(DomainModel.SurveyModel.Survey survey)
    {
        var validationResult = _surveyValidator.Validate(survey);
        if (!validationResult.IsSuccessful)
        {
            _logger.LogWarning($"Validation is failed : {validationResult.Error.GetValueOrDefault()}.");

            return OperationResult<SurveyManagementErrors>.CreateUnsuccessful(validationResult.Error.GetValueOrDefault());
        }

        var creationResult = await _surveyStore.CreateAsync(survey);
        if (!creationResult.IsSuccessful)
        {
            _logger.LogWarning($"Creation is failed: {creationResult.Error.GetValueOrDefault()}.");

            return OperationResult<SurveyManagementErrors>.CreateUnsuccessful(creationResult.Error.GetValueOrDefault());
        }

        return OperationResult<SurveyManagementErrors>.CreateSuccessful();
    }

    public async Task<OperationResult<SurveyManagementErrors>> EditSurveyAsync(DomainModel.SurveyModel.Survey survey)
    {
        var validationResult = _surveyValidator.Validate(survey);
        if (!validationResult.IsSuccessful)
        {
            _logger.LogWarning($"Validation is failed : {validationResult.Error.GetValueOrDefault()}.");

            return OperationResult<SurveyManagementErrors>.CreateUnsuccessful(validationResult.Error.GetValueOrDefault());
        }

        var updatingResult = await _surveyStore.UpdateAsync(survey);
        if (!updatingResult.IsSuccessful)
        {
            _logger.LogWarning($"Updating is failed: {updatingResult.Error.GetValueOrDefault()}.");

            return OperationResult<SurveyManagementErrors>.CreateUnsuccessful(updatingResult.Error.GetValueOrDefault());
        }

        return OperationResult<SurveyManagementErrors>.CreateSuccessful();
    }

    public async Task<OperationResult<SurveyManagementErrors>> DeleteSurveyAsync(DomainModel.SurveyModel.Survey survey)
    {
        var deletengResult = await _surveyStore.DeleteAsync(survey);
        if (!deletengResult.IsSuccessful)
        {
            _logger.LogWarning($"Deleting is failed: {deletengResult.Error.GetValueOrDefault()}.");

            return OperationResult<SurveyManagementErrors>.CreateUnsuccessful(deletengResult.Error.GetValueOrDefault());
        }

        return OperationResult<SurveyManagementErrors>.CreateSuccessful();
    }

    public async Task<OperationResult<SurveyManagementErrors>> DeleteSurveyByIdAsync(long surveyId)
    {
        var deletengResult = await _surveyStore.DeleteByIdAsync(surveyId);
        if (!deletengResult.IsSuccessful)
        {
            _logger.LogWarning($"Deleting is failed: {deletengResult.Error.GetValueOrDefault()}.");

            return OperationResult<SurveyManagementErrors>.CreateUnsuccessful(SurveyManagementErrors.SurveyDeletingIsFailed);
        }

        return OperationResult<SurveyManagementErrors>.CreateSuccessful();
    }

    public async Task<DomainModel.SurveyModel.Survey> GetSurveyByIdAsync(long surveyId)
    {
        var survey = await _surveyStore.FindByIdAsync(surveyId);

        return survey;
    }

    public async Task<IList<DomainModel.SurveyModel.Survey>> GetSurveysByTitleAsync(string title)
    {
        var surveysCollection = await _surveyStore.FindByTitleAsync(title);

        return surveysCollection;
    }

    public async Task<IList<DomainModel.SurveyModel.Survey>> GetAllSurveysByUserIdAsync(int userId)
    {
        var surveys = await _surveyStore.FindSurveysByUserIdAsync(userId);

        return surveys;
    }
}