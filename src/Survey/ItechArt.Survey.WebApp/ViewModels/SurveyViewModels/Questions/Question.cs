namespace ItechArt.Survey.WebApp.ViewModels.SurveyViewModels.Questions;

public class QuestionViewModel
{
    public const int TextMaxLength = 500;


    public long Id { get; set; }

    public string Text { get; set; }

    public long SurveyId { get; set; }


    public DomainModel.SurveyModel.Survey Survey { get; set; }
}