using System.Collections.Generic;
using System.Threading.Tasks;
using ItechArt.Common;
using ItechArt.Common.Logging.Abstractions;
using ItechArt.Common.Logging.Extensions;
using ItechArt.Survey.DomainModel.SurveyModel.Questions;
using ItechArt.Survey.Foundation.QuestionManagement.Abstractions;
using ItechArt.Survey.Foundation.QuestionManagement.Stores.Abstractions;
using ItechArt.Survey.Foundation.SurveyManagement.Abstractions;
using ItechArt.Survey.Foundation.SurveyManagement.Stores.Abstractions;
using ItechArt.Survey.Foundation.SurveyManagement.Validation.Abstractions;

namespace ItechArt.Survey.Foundation.QuestionManagement;

public class QuestionService : IQuestionService
{
    private IQuestionStore _questionStore;
    private ILogger _logger;
    private ISurveyValidator _surveyValidator;

    public QuestionService(
        IQuestionStore questionStore,
        ILogger logger,
        ISurveyValidator surveyValidator, IAnswerStore answerStore)
    {
        _questionStore = questionStore;
        _logger = logger;
        _surveyValidator = surveyValidator;
    }


    public async Task<OperationResult<QuestionManagementErrors>> CreateQuestion<TQuestion>(TQuestion question)
        where TQuestion : Question
    {
        var creatingResult = await _questionStore.CreateAsync(question);
        if (!creatingResult.IsSuccessful)
        {
            return OperationResult<QuestionManagementErrors>.CreateUnsuccessful(QuestionManagementErrors.CreationQuestionIsFailed);
        }

        return OperationResult<QuestionManagementErrors>.CreateSuccessful();
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
}