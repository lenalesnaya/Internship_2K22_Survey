namespace ItechArt.Survey.DomainModel.Survey.Questions;

public class Question
{
    public const int TextMaxLength = 500;


    public long Id { get; set; }

    public long SurveyId { get; set; }

    public string Text { get; set; }


    public SurveyModel.Survey Survey { get; set; }
}