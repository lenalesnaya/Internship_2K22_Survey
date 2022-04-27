using System.Linq;
using AutoMapper;
using ItechArt.Survey.DomainModel.SurveyModel.Answers;
using ItechArt.Survey.DomainModel.SurveyModel.Questions;
using ItechArt.Survey.WebApp.ViewModels.SurveyViewModels.Questions;

namespace ItechArt.Survey.WebApp.ViewModels.Mapping;

public class QuestionViewModelProfile : Profile
{
    public QuestionViewModelProfile()
    {
        CreateMap<AnswerVariantsQuestionViewModel, AnswerVariantsQuestion>()
            .ForMember(d => d.AnswerVariants,
                opt =>
                    opt.MapFrom(s => s.AnswerVariants.Select(el =>
                        new AnswerVariant
                        {
                            Title = el.Title
                        }
                    ))).ReverseMap();
    }
}