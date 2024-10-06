using AutoMapper;
using Domain.Entities;
using Presentation.Entities.Creation;
using Presentation.Entities.View;

namespace Presentation.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserForCreation, User>();
        CreateMap<User, UserForView>();
    }
}
