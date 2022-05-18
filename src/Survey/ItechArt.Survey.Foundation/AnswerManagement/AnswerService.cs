using System.Collections.Generic;
using System.Threading.Tasks;
using ItechArt.Common;
using ItechArt.Common.Logging.Abstractions;
using ItechArt.Common.Logging.Extensions;
using ItechArt.Survey.DomainModel.SurveyModel.Answers;
using ItechArt.Survey.DomainModel.SurveyModel.Questions;
using ItechArt.Survey.Foundation.AnswerManagement.Abstrations;
using ItechArt.Survey.Foundation.SurveyManagement.Abstractions;
using ItechArt.Survey.Foundation.SurveyManagement.Stores.Abstractions;
using ItechArt.Survey.Foundation.SurveyManagement.Validation.Abstractions;

namespace ItechArt.Survey.Foundation.AnswerManagement;

public class AnswerService : IAnswerService
{
    private ISurveyValidator _surveyValidator;
    private ILogger _logger;
    private IAnswerStore _answerStore;

    public AnswerService(
        ISurveyValidator surveyValidator,
        ILogger logger,
        IAnswerStore answerStore)
    {
        _surveyValidator = surveyValidator;
        _logger = logger;
        _answerStore = answerStore;
    }

    public async Task<OperationResult<SurveyManagementErrors>> CreateAnswerVariantAsync(AnswerVariant answer)
    {
        var validationResult = _surveyValidator.Validate(answer);
        if (!validationResult.IsSuccessful)
        {
            _logger.LogWarning($"Validation is failed : {validationResult.Error.GetValueOrDefault()}.");

            return OperationResult<SurveyManagementErrors>.CreateUnsuccessful(validationResult.Error.GetValueOrDefault());
        }

        var creationResult = await _answerStore.CreateAsync(answer);
        if (!creationResult.IsSuccessful)
        {
            _logger.LogWarning($"Creation is failed: {creationResult.Error.GetValueOrDefault()}.");

            return OperationResult<SurveyManagementErrors>.CreateUnsuccessful(creationResult.Error.GetValueOrDefault());
        }

        return OperationResult<SurveyManagementErrors>.CreateSuccessful();
    }

    public async Task<OperationResult<SurveyManagementErrors>> EditAnswerVariantAsync(AnswerVariant answer)
    {
        var validationResult = _surveyValidator.Validate(answer);
        if (!validationResult.IsSuccessful)
        {
            _logger.LogWarning($"Validation is failed : {validationResult.Error.GetValueOrDefault()}.");

            return OperationResult<SurveyManagementErrors>.CreateUnsuccessful(validationResult.Error.GetValueOrDefault());
        }

        var updatingResult = await _answerStore.UpdateAsync(answer);
        if (!updatingResult.IsSuccessful)
        {
            _logger.LogWarning($"Updating is failed: {updatingResult.Error.GetValueOrDefault()}.");

            return OperationResult<SurveyManagementErrors>.CreateUnsuccessful(updatingResult.Error.GetValueOrDefault());
        }

        return OperationResult<SurveyManagementErrors>.CreateSuccessful();
    }

    public async Task<OperationResult<SurveyManagementErrors>> DeleteAnswerVariantAsync(AnswerVariant answer)
    {
        var deletengResult = await _answerStore.DeleteAsync(answer);
        if (!deletengResult.IsSuccessful)
        {
            _logger.LogWarning($"Deleting is failed: {deletengResult.Error.GetValueOrDefault()}.");

            return OperationResult<SurveyManagementErrors>.CreateUnsuccessful(deletengResult.Error.GetValueOrDefault());
        }

        return OperationResult<SurveyManagementErrors>.CreateSuccessful();
    }

    public async Task<AnswerVariant> GetAnswerVariantByIdAsync(long answerId)
    {
        var answer = await _answerStore.FindByIdAsync(answerId);

        return answer;
    }

    public async Task<IList<AnswerVariant>> GetAnswerVariantsByQuestionAsync(AnswerVariantsQuestion question)
    {
        var answers = await _answerStore.FindAnswerVariantsByQuestionAsync(question);

        return answers;
    }
}