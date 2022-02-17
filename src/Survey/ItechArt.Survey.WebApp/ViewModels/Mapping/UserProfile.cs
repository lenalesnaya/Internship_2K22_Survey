using AutoMapper;
using ItechArt.Survey.DomainModel;

namespace ItechArt.Survey.WebApp.ViewModels.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<ProfileViewModel, User>();
        CreateMap<User, ProfileViewModel>();
    }
}