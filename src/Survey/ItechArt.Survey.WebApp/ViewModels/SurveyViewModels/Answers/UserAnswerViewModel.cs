using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ItechArt.Survey.WebApp.ViewModels.SurveyViewModels.Answers;

public class UserAnswerViewModel
{
    public long SurveyId { get; set; }

    public IList<AnswerVariantViewModel> UserAnswers { get; set; }
}