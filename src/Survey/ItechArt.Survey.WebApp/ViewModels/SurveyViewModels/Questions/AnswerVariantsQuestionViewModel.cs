using System.Collections.Generic;
using ItechArt.Survey.WebApp.ViewModels.SurveyViewModels.Answers;

namespace ItechArt.Survey.WebApp.ViewModels.SurveyViewModels.Questions;

public class AnswerVariantsQuestionViewModel
{
    public long Id { get; set; }

    public bool CanChooseManyAnswers { get; set; }

    public string Title { get; set; }

    public long SurveyId { get; set; }

    public IList<AnswerVariantViewModel> AnswerVariants { get; set; }
}