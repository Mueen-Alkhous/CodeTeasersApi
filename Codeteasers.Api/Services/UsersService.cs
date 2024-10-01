using Domain.Entities;
using Web.Api.Entities.Creation;

namespace Web.Api.Services;

public class UsersService
{
    public User CreateUserWithStatus(UserForCreation user)
    {
        var commonId = Guid.NewGuid();
        var newUserStatus = new UserStatus
        {
            Id = commonId,
        };

        var newUser = new User
        {
            Id = commonId,
            UserStatus = newUserStatus,
            Username = user.Username,
            Email = user.Email,
            Password = user.Password,
        };
        return newUser;

    }
}
