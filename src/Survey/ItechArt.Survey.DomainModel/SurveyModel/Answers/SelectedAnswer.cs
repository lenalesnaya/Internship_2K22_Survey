using ItechArt.Survey.DomainModel.SurveyModel.Questions;
using ItechArt.Survey.DomainModel.UserModel;

namespace ItechArt.Survey.DomainModel.SurveyModel.Answers;

public class SelectedAnswer
{
    public long Id { get; set; }


    public virtual AnswerVariant AnswerVariant { get; set; }

    public virtual User User { get; set; }
}