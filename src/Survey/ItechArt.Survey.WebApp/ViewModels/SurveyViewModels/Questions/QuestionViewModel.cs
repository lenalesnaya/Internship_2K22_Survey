using System.Collections.Generic;

namespace ItechArt.Survey.WebApp.ViewModels.SurveyViewModels.Questions;

public class QuestionViewModel
{
    public string Title { get; set; }

    public long SurveyId { get; set; }

    public IList<string> Answers { get; set; }
}