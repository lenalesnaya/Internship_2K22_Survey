using ItechArt.Survey.DomainModel.Survey.Questions;

namespace ItechArt.Survey.DomainModel.Survey.Answers;

public class AnswerVariant
{
    public const int TextMaxLength = 500;


    public long Id { get; set; }

    public long QuestionId { get; set; }

    public long SurveyId { get; set; }

    public string Text { get; set; }


    public AnswerVariantsQuestion Question { get; set; }
}