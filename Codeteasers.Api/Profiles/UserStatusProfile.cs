using AutoMapper;
using Domain.Entities;
using Presentation.Entities.View;

namespace Presentation.Profiles;

public class UserStatusProfile : Profile
{
    public UserStatusProfile()
    {
        CreateMap<UserStatus, UserStatusForView>();
    }
}
