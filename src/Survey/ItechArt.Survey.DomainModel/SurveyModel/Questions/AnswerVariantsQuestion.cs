using ItechArt.Survey.DomainModel.SurveyModel.Answers;
using System.Collections.Generic;

namespace ItechArt.Survey.DomainModel.SurveyModel.Questions;

public class AnswerVariantsQuestion : Question
{
    public const int VariantsMaxQuantity = 20;


    public bool CanChooseManyAnswers { get; set; }


    public ICollection<AnswerVariant> AnswerVariants { get; set; }
}