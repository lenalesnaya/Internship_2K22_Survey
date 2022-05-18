using AutoMapper;
using ItechArt.Survey.DomainModel.UserModel;
using ItechArt.Survey.WebApp.ViewModels.UserViewModels;

namespace ItechArt.Survey.WebApp.ViewModels.Mapping;

public class UserViewModelProfile : Profile
{
    public UserViewModelProfile()
    {
        CreateMap<User, UserViewModel>();
    }
}