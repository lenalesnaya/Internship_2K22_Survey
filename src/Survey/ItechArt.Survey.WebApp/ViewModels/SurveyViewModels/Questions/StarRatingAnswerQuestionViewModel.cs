namespace ItechArt.Survey.WebApp.ViewModels.SurveyViewModels.Questions;

public class StarRatingAnswerQuestionViewModel
{
    public const int StarsMaxQuantity = 20;


    public string Title { get; set; }

    public long SurveyId { get; set; }

    public int AmountOfStars { get; set; }
}