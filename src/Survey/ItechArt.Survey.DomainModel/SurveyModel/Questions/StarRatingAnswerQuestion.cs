namespace ItechArt.Survey.DomainModel.SurveyModel.Questions;

public class StarRatingAnswerQuestion : Question
{
    public const int StarsMinAmount = 2;
    public const int StarsMaxAmount = 20;


    public int AmountOfStars { get; set; }
}