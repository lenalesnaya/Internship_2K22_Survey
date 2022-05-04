using System.ComponentModel.DataAnnotations.Schema;
using ItechArt.Survey.DomainModel.UserModel;


namespace ItechArt.Survey.DomainModel.SurveyModel.Answers;

public class UserAnswer
{
    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    public int UserId { get; set; }

    public long AnswerVariantId { get; set; }

    public virtual AnswerVariant AnswerVariant { get; set; }

    public virtual User User { get; set; }
}