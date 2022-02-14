using AutoMapper;

namespace ItechArt.Survey.WebApp.ViewModels.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<ProfileViewModel, RegistrationViewModel>();
    }
}