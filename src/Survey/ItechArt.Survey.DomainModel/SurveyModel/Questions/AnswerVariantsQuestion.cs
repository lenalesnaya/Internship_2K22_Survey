using ItechArt.Survey.DomainModel.Survey.Answers;
using System.Collections.Generic;

namespace ItechArt.Survey.DomainModel.Survey.Questions;

public class AnswerVariantsQuestion : Question
{
    public const int VariantsMaxQuantity = 20;


    public bool CanChooseManyAnswers { get; set; } = false;


    public ICollection<AnswerVariant> AnswerVariants { get; set; }
}