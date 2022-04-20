using ItechArt.Survey.WebApp.ViewModels.SurveyViewModels.Questions;

namespace ItechArt.Survey.WebApp.ViewModels.SurveyViewModels.Answers;

public class AnswerVariantViewModel
{
    public long Id { get; set; }

    public long QuestionId { get; set; }

    public long SurveyId { get; set; }

    public string Text { get; set; }


    public AnswerVariantsQuestionViewModel Question { get; set; }
}