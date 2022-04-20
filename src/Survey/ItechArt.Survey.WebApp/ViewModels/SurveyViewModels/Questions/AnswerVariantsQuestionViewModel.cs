using System.Collections.Generic;
using ItechArt.Survey.DomainModel.SurveyModel.Questions;
using ItechArt.Survey.WebApp.ViewModels.SurveyViewModels.Answers;

namespace ItechArt.Survey.WebApp.ViewModels.SurveyViewModels.Questions;

public class AnswerVariantsQuestionViewModel : Question
{
    public const int VariantsMaxQuantity = 20;


    public bool CanChooseManyAnswers { get; set; }


    public ICollection<AnswerVariantViewModel> AnswerVariants { get; set; }
}