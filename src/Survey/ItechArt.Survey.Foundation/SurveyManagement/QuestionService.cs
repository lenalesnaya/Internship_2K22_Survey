using System.Threading.Tasks;
using ItechArt.Common;
using ItechArt.Survey.DomainModel.SurveyModel.Questions;
using ItechArt.Survey.Foundation.SurveyManagement.Abstractions;
using ItechArt.Survey.Foundation.SurveyManagement.Stores;
using ItechArt.Survey.Foundation.SurveyManagement.Stores.Abstractions;

namespace ItechArt.Survey.Foundation.SurveyManagement;

public class QuestionService : IQuestionService
{
    private IQuestionStore _questionStore;


    public QuestionService(IQuestionStore questionStore)
    {
        _questionStore = questionStore;
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
}