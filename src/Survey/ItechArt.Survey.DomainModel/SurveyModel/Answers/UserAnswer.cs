using ItechArt.Survey.DomainModel.UserModel;


namespace ItechArt.Survey.DomainModel.SurveyModel.Answers;

public class UserAnswer
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public long AnswerVariantId { get; set; }


    public virtual AnswerVariant AnswerVariant { get; set; }

    public virtual User User { get; set; }
}