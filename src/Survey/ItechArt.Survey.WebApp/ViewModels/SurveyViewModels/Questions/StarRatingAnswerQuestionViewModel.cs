namespace ItechArt.Survey.WebApp.ViewModels.SurveyViewModels.Questions;

public class StarRatingAnswerQuestionViewModel : QuestionViewModel
{
    public const int StarsMaxQuantity = 20;


    public int AmountOfStars { get; set; }
}