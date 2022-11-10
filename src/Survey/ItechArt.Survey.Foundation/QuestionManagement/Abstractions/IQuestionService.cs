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
}