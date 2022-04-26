using System.Collections.Generic;
using System.Threading.Tasks;
using ItechArt.Common;
using ItechArt.Common.Logging.Abstractions;
using ItechArt.Common.Logging.Extensions;
using ItechArt.Survey.DomainModel.SurveyModel.Answers;
using ItechArt.Survey.DomainModel.SurveyModel.Questions;
using ItechArt.Survey.Foundation.SurveyManagement.Abstractions;
using ItechArt.Survey.Foundation.SurveyManagement.Stores.Abstractions;
using ItechArt.Survey.Foundation.SurveyManagement.Validation.Abstractions;

namespace ItechArt.Survey.Foundation.SurveyManagement;

public class SurveyService : ISurveyService
{
    private readonly ISurveyStore _surveyStore;
    private readonly IQuestionStore _questionStore;
    private readonly IAnswerStore _answerStore;
    private readonly ILogger _logger;
    private readonly ISurveyValidator _surveyValidator;

    public SurveyService(
        ISurveyStore surveyStore,
        ILogger logger,
        IQuestionStore questionStore,
        ISurveyValidator surveyValidator,
        IAnswerStore answerStore)
    {
        _surveyStore = surveyStore;
        _logger = logger;
        _questionStore = questionStore;
        _surveyValidator = surveyValidator;
        _answerStore = answerStore;
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

    public async Task<OperationResult<SurveyManagementErrors>> CreateQuestionAsync<TypeOfQuestion>(TypeOfQuestion question)
        where TypeOfQuestion : Question
    {
        var validationResult = _surveyValidator.ValidateQuestion(question);
        if (!validationResult.IsSuccessful)
        {
            _logger.LogWarning($"Validation is failed : {validationResult.Error.GetValueOrDefault()}.");

            return OperationResult<SurveyManagementErrors>.CreateUnsuccessful(validationResult.Error.GetValueOrDefault());
        }

        var creationResult = await _questionStore.CreateAsync(question);
        if (!creationResult.IsSuccessful)
        {
            _logger.LogWarning($"Creation is failed: {creationResult.Error.GetValueOrDefault()}.");

            return OperationResult<SurveyManagementErrors>.CreateUnsuccessful(creationResult.Error.GetValueOrDefault());
        }

        return OperationResult<SurveyManagementErrors>.CreateSuccessful();
    }

    public async Task<OperationResult<SurveyManagementErrors>> EditQuestionAsync<TypeOfQuestion>(TypeOfQuestion question)
        where TypeOfQuestion : Question
    {
        var validationResult = _surveyValidator.ValidateQuestion(question);
        if (!validationResult.IsSuccessful)
        {
            _logger.LogWarning($"Validation is failed : {validationResult.Error.GetValueOrDefault()}.");

            return OperationResult<SurveyManagementErrors>.CreateUnsuccessful(validationResult.Error.GetValueOrDefault());
        }

        var updatingResult = await _questionStore.UpdateAsync(question);
        if (!updatingResult.IsSuccessful)
        {
            _logger.LogWarning($"Updating is failed: {updatingResult.Error.GetValueOrDefault()}.");

            return OperationResult<SurveyManagementErrors>.CreateUnsuccessful(updatingResult.Error.GetValueOrDefault());
        }

        return OperationResult<SurveyManagementErrors>.CreateSuccessful();
    }

    public async Task<OperationResult<SurveyManagementErrors>> DeleteQuestionAsync<TypeOfQuestion>(TypeOfQuestion question)
        where TypeOfQuestion : Question
    {
        var deletengResult = await _questionStore.DeleteAsync(question);
        if (!deletengResult.IsSuccessful)
        {
            _logger.LogWarning($"Deleting is failed: {deletengResult.Error.GetValueOrDefault()}.");

            return OperationResult<SurveyManagementErrors>.CreateUnsuccessful(deletengResult.Error.GetValueOrDefault());
        }

        return OperationResult<SurveyManagementErrors>.CreateSuccessful();
    }

    public async Task<TypeOfQuestion> GetQuestionByIdAsync<TypeOfQuestion>(long questionId)
        where TypeOfQuestion : Question
    {
        var question = await _questionStore.FindByIdAsync<TypeOfQuestion>(questionId);

        return question;
    }

    public async Task<IList<TypeOfQuestion>> GetOneTypeQuestionsBySurveyIdAsync<TypeOfQuestion>(long surveyId)
    where TypeOfQuestion : Question
    {
        var questions = await _questionStore.FindOneTypeQuestionsBySurveyIdAsync<TypeOfQuestion>(surveyId);

        return questions;
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