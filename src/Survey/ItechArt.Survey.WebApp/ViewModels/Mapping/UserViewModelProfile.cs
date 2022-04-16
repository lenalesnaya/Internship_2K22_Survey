using AutoMapper;
using ItechArt.Survey.DomainModel.UserModel;

namespace ItechArt.Survey.WebApp.ViewModels.Mapping;

public class UserViewModelProfile : Profile
{
    public UserViewModelProfile()
    {
        CreateMap<User, UserViewModel>();
    }
}