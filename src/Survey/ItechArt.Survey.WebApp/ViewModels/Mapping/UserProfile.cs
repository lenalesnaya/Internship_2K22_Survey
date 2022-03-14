using AutoMapper;
using ItechArt.Survey.DomainModel;

namespace ItechArt.Survey.WebApp.ViewModels.Mapping;

public class ProfileViewModelProfile : Profile
{
    public ProfileViewModelProfile()
    {
        CreateMap<User, ProfileViewModel>()
            .ForMember(p => p.User,
                opt => opt.MapFrom(src => src))
            .ReverseMap();
    }
}