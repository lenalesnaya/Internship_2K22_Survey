using System.Collections.Generic;
using System.Threading.Tasks;
using ItechArt.Common;
using ItechArt.Survey.DomainModel.SurveyModel.Questions;
using ItechArt.Survey.Foundation.SurveyManagement.Abstractions;

namespace ItechArt.Survey.Foundation.QuestionManagement.Abstractions;

public interface IQuestionService
{
    Task<OperationResult<QuestionManagementErrors>> CreateQuestion<TQuestion>(TQuestion question)
        where TQuestion : Question;

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
}