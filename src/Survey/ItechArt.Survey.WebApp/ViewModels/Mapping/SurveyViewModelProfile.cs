using AutoMapper;
using ItechArt.Survey.WebApp.ViewModels.SurveyViewModels;

namespace ItechArt.Survey.WebApp.ViewModels.Mapping;

public class SurveyViewModelProfile : Profile
{
    public SurveyViewModelProfile()
    {
        CreateMap<SurveyViewModel, DomainModel.SurveyModel.Survey>();
        CreateMap<DomainModel.SurveyModel.Survey, SurveyViewModel>();
    }
}