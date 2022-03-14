using AutoMapper;
using ItechArt.Survey.DomainModel;

namespace ItechArt.Survey.WebApp.ViewModels.Mapping;

public class UserViewModelProfile : Profile
{
    public UserViewModelProfile()
    {
        CreateMap<UserViewModel, User>();
        CreateMap<User, UserViewModel>();
    }
}