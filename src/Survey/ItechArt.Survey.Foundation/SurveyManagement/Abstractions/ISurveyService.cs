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

    Task<OperationResult<SurveyManagementErrors>> CreateQuestionAsync<TypeOfQuestion>(TypeOfQuestion question)
        where TypeOfQuestion : Question;

    Task<OperationResult<SurveyManagementErrors>> EditQuestionAsync<TypeOfQuestion>(TypeOfQuestion question)
        where TypeOfQuestion : Question;

    Task<OperationResult<SurveyManagementErrors>> DeleteQuestionAsync<TypeOfQuestion>(TypeOfQuestion question)
        where TypeOfQuestion : Question;

    Task<TypeOfQuestion> GetQuestionByIdAsync<TypeOfQuestion>(long questionId)
        where TypeOfQuestion : Question;

    Task<IList<TypeOfQuestion>> GetOneTypeQuestionsBySurveyIdAsync<TypeOfQuestion>(long surveyId)
        where TypeOfQuestion : Question;

    Task<OperationResult<SurveyManagementErrors>> CreateAnswerVariantAsync(AnswerVariant answer);

    Task<OperationResult<SurveyManagementErrors>> EditAnswerVariantAsync(AnswerVariant answer);

    Task<OperationResult<SurveyManagementErrors>> DeleteAnswerVariantAsync(AnswerVariant answer);

    Task<AnswerVariant> GetAnswerVariantByIdAsync(long answerId);

    Task<IList<AnswerVariant>> GetAnswerVariantsByQuestionAsync(AnswerVariantsQuestion question);
}