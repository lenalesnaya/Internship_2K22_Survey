namespace ItechArt.Survey.DomainModel.SurveyModel.Questions;

public class Question
{
    public const int TextMaxLength = 500;


    public long Id { get; set; }

    public string Text { get; set; }

    public long SurveyId { get; set; }


    public Survey Survey { get; set; }
}