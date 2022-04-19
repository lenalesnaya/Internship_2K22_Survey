namespace ItechArt.Survey.DomainModel.SurveyModel.Questions;

public class StarRatingAnswerQuestion : Question
{
    public const int StarsMaxQuantity = 20;


    public int NumberOfStars { get; set; }
}