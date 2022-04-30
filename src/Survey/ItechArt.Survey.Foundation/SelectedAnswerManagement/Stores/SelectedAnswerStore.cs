using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ItechArt.Common;
using ItechArt.Repositories;
using ItechArt.Repositories.Abstractions;
using ItechArt.Survey.DomainModel.SurveyModel.Answers;
using ItechArt.Survey.Foundation.SelectedAnswerManagement.Abstractions;
using ItechArt.Survey.Foundation.SelectedAnswerManagement.Stores.Abstractions;

namespace ItechArt.Survey.Foundation.SelectedAnswerManagement.Stores;

public class SelectedAnswerStore : ISelectedAnswerStores
{
    private IUnitOfWork _unitOfWork;


    public SelectedAnswerStore(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<OperationResult<SelectedAnswerErrors>> AddSelectedAnswer(IList<SelectedAnswer> answers)
    {
        var answerRepository = _unitOfWork.GetRepository<SelectedAnswer>();
        try
        {
            foreach (var answer in answers)
            {
                answerRepository.Add(answer);
            }
            await _unitOfWork.SaveChangesAsync();

            return OperationResult<SelectedAnswerErrors>.CreateSuccessful();
        }
        catch (Exception e)
        {
            return OperationResult<SelectedAnswerErrors>.CreateUnsuccessful(SelectedAnswerErrors.AddAnswerIsFailed);
        }
    }
}