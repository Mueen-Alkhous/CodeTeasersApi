using Domain.Entities;
using Presentation.Entities.Creation;

namespace Presentation.Services;

public class UserService
{
    public User CreateUserWithStatus(UserForCreation user)
    {
        var newUser = new User(user.Username, user.Email, user.Password);

        
        return newUser;
    }
}
