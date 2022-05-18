using System.Collections.Generic;
using System.Threading.Tasks;
using ItechArt.Common;
using ItechArt.Survey.DomainModel.SurveyModel.Questions;
using ItechArt.Survey.Foundation.SurveyManagement.Abstractions;

namespace ItechArt.Survey.Foundation.QuestionManagement.Stores.Abstractions;

public interface IQuestionStore
{
     Task<OperationResult<SurveyManagementErrors>> CreateAsync<TypeOfQuestion>(TypeOfQuestion question)
         where TypeOfQuestion : Question;

    Task<OperationResult<SurveyManagementErrors>> UpdateAsync<TypeOfQuestion>(TypeOfQuestion question)
        where TypeOfQuestion : Question;

    Task<OperationResult<SurveyManagementErrors>> DeleteAsync<TypeOfQuestion>(TypeOfQuestion question)
        where TypeOfQuestion : Question;

    Task<TypeOfQuestion> FindByIdAsync<TypeOfQuestion>(long questionId)
        where TypeOfQuestion : Question;

    Task<IList<TypeOfQuestion>> FindOneTypeQuestionsBySurveyIdAsync<TypeOfQuestion>(long id)
        where TypeOfQuestion : Question;
}