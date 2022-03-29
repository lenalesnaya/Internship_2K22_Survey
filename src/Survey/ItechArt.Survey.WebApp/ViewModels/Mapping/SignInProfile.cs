using AutoMapper;
using ItechArt.Survey.DomainModel;

namespace ItechArt.Survey.WebApp.ViewModels.Mapping;

public class SignInProfile : Profile
{
    public SignInProfile()
    {
        CreateMap<SignInViewModel, User>();
    }
}