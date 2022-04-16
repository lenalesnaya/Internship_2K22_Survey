namespace ItechArt.Survey.DomainModel.Survey.Questions;

public class StarRatingAnswerQuestion : Question
{
    public const int StarsMaxQuantity = 20;


    public int NumberOfStars { get; set; }
}