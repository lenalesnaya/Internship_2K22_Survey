using AutoMapper;
using ItechArt.Survey.DomainModel;

namespace ItechArt.Survey.WebApp.ViewModels.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, ProfileViewModel>()
            .ForMember(p => p.User,
                opt => opt.MapFrom(src => src))
            .ReverseMap();
    }
}