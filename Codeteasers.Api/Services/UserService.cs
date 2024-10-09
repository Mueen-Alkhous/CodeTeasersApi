using Domain.Entities;
using Presentation.Entities.Creation;

namespace Presentation.Services;

public class UserService
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
            Username = user.Username,
            Email = user.Email,
            Password = user.Password,
            UserStatus = newUserStatus,
        };
        return newUser;
    }
}
