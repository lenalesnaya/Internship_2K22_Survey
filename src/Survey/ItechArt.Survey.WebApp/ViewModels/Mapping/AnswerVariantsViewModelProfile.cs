using AutoMapper;
using ItechArt.Survey.DomainModel.SurveyModel.Answers;
using ItechArt.Survey.WebApp.ViewModels.SurveyViewModels.Answers;

namespace ItechArt.Survey.WebApp.ViewModels.Mapping;

public class AnswerVariantsViewModelProfile : Profile
{
    public AnswerVariantsViewModelProfile()
    {
        CreateMap<AnswerVariant, AnswerVariantViewModel>()
            .ReverseMap();
    }
}