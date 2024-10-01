using AutoMapper;
using Domain.Entities;
using Web.Api.Entities.Creation;
using Web.Api.Entities.View;

namespace Web.Api.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserForCreation, User>();
        CreateMap<User, UserForView>();
    }
}
