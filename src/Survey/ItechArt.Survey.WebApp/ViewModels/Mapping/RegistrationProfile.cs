using AutoMapper;
using ItechArt.Survey.DomainModel;

namespace ItechArt.Survey.WebApp.ViewModels.Mapping;

public class RegistrationProfile : Profile
{
    public RegistrationProfile()
    {
        CreateMap<RegistrationViewModel, User>();
    }
}