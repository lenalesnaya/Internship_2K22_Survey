using System.Collections.Generic;
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
        CreateMap<AnswerVariantViewModel, UserAnswer>()
            .ForMember(d=>d.Id, opt
                =>opt.Ignore())
            .ForMember(d=>d.AnswerVariantId, opt
                =>opt.MapFrom(s=>s.Id));
    }
}