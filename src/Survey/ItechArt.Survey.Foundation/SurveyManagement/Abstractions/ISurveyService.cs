using System.Collections.Generic;
using System.Threading.Tasks;
using ItechArt.Common;
using ItechArt.Survey.DomainModel.SurveyModel.Answers;
using ItechArt.Survey.DomainModel.SurveyModel.Questions;

namespace ItechArt.Survey.Foundation.SurveyManagement.Abstractions;

public interface ISurveyService
{
    Task<OperationResult<SurveyManagementErrors>> CreateSurvey(string title, int creatorId);

    Task<OperationResult<SurveyManagementErrors>> EditSurvey(DomainModel.SurveyModel.Survey survey);

    Task<OperationResult<SurveyManagementErrors>> DeleteSurvey(DomainModel.SurveyModel.Survey survey);

    Task<DomainModel.SurveyModel.Survey> GetSurveyByIdAsync(long surveyId);

    Task<IList<DomainModel.SurveyModel.Survey>> GetSurveysByTitleAsync(string title);

    Task<IList<DomainModel.SurveyModel.Survey>> GetAllSurveysByUserId(int userId);

    Task<OperationResult<SurveyManagementErrors>> CreateQuestion<TypeOfQuestion>(TypeOfQuestion question)
        where TypeOfQuestion : Question;

    Task<OperationResult<SurveyManagementErrors>> EditQuestion<TypeOfQuestion>(TypeOfQuestion question)
        where TypeOfQuestion : Question;

    Task<OperationResult<SurveyManagementErrors>> DeleteQuestion<TypeOfQuestion>(TypeOfQuestion question)
        where TypeOfQuestion : Question;

    Task<TypeOfQuestion> GetQuestionByIdAsync<TypeOfQuestion>(long questionId)
        where TypeOfQuestion : Question;

    Task<IList<TypeOfQuestion>> GetOneTypeQuestionsBySurveyIdAsync<TypeOfQuestion>(long surveyId)
        where TypeOfQuestion : Question;

    Task<OperationResult<SurveyManagementErrors>> CreateAnswerVariant(AnswerVariant answer);

    Task<OperationResult<SurveyManagementErrors>> EditAnswerVariant(AnswerVariant answer);

    Task<OperationResult<SurveyManagementErrors>> DeleteAnswerVariant(AnswerVariant answer);

    Task<AnswerVariant> GetAnswerVariantByIdAsync(long answerId);

    Task<IList<AnswerVariant>> GetAnswerVariantsByQuestionAsync(AnswerVariantsQuestion question);
}