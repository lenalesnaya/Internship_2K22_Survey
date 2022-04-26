using System.Threading.Tasks;
using ItechArt.Common;
using ItechArt.Survey.DomainModel.SurveyModel.Questions;

namespace ItechArt.Survey.Foundation.SurveyManagement.Abstractions;

public interface IQuestionService
{
    Task<OperationResult<QuestionManagementErrors>> CreateQuestion<TQuestion>(TQuestion question)
        where TQuestion : Question;
}