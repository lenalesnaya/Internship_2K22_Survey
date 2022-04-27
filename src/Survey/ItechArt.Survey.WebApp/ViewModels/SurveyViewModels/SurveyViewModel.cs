using System.Collections.Generic;
using ItechArt.Survey.WebApp.ViewModels.SurveyViewModels.Questions;

namespace ItechArt.Survey.WebApp.ViewModels.SurveyViewModels;

public class SurveyViewModel
{
    public long Id { get; set; }

    public string Title { get; set; }

    public bool IsAnonymous { get; set; }

    public IList<AnswerVariantsQuestionViewModel> AnswerVariantsQuestions { get; set; }
}