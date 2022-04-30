using System.Collections.Generic;

namespace ItechArt.Survey.WebApp.ViewModels.SurveyViewModels.Answers;

public class SelectedAnswerViewModel
{
    public long QuestionId { get; set; }


    public IList<string> Answers { get; set; }
}