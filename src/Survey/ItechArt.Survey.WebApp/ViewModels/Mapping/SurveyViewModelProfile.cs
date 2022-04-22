using AutoMapper;

namespace ItechArt.Survey.WebApp.ViewModels.Mapping;

public class SurveyViewModelProfile : Profile
{
    public SurveyViewModelProfile()
    {
        CreateMap<SurveyViewModelProfile, DomainModel.SurveyModel.Survey>();
        CreateMap<DomainModel.SurveyModel.Survey, SurveyViewModelProfile>();
    }
}