using System.Collections.Generic;
using System.Threading.Tasks;
using ItechArt.Common;
using ItechArt.Survey.DomainModel.SurveyModel.Answers;
using ItechArt.Survey.Foundation.SelectedAnswerManagement.Abstractions;
using ItechArt.Survey.Foundation.SelectedAnswerManagement.Stores.Abstractions;

namespace ItechArt.Survey.Foundation.SelectedAnswerManagement;

public class SelectedAnswerService : ISelectedAnswerService
{
    private ISelectedAnswerStores _selectedAnswerStores;


    public SelectedAnswerService(ISelectedAnswerStores selectedAnswerStores)
    {
        _selectedAnswerStores = selectedAnswerStores;
    }


    public async Task<OperationResult<SelectedAnswerErrors>> AddSelectedAnswers(IList<SelectedAnswer> answers)
    {
        var result = await _selectedAnswerStores.AddSelectedAnswer(answers);
        if (!result.IsSuccessful)
        {
            return OperationResult<SelectedAnswerErrors>.CreateUnsuccessful(SelectedAnswerErrors.AddAnswerIsFailed);
        }

        return OperationResult<SelectedAnswerErrors>.CreateSuccessful();
    }
}