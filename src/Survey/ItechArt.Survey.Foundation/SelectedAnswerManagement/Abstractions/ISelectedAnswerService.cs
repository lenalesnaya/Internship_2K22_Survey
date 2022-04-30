using System.Collections.Generic;
using System.Threading.Tasks;
using ItechArt.Common;
using ItechArt.Survey.DomainModel.SurveyModel.Answers;

namespace ItechArt.Survey.Foundation.SelectedAnswerManagement.Abstractions;

public interface ISelectedAnswerService
{
    Task<OperationResult<SelectedAnswerErrors>> AddSelectedAnswers(IList<SelectedAnswer> answers);
}