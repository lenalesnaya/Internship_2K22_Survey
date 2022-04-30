using System.Collections.Generic;
using System.Threading.Tasks;
using ItechArt.Common;
using ItechArt.Survey.DomainModel.SurveyModel.Answers;
using ItechArt.Survey.Foundation.SelectedAnswerManagement.Abstractions;

namespace ItechArt.Survey.Foundation.SelectedAnswerManagement.Stores.Abstractions;

public interface ISelectedAnswerStores
{
    Task<OperationResult<SelectedAnswerErrors>> AddSelectedAnswer(IList<SelectedAnswer> answers);
}