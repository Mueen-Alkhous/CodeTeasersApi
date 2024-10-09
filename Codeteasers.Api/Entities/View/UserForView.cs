using Domain.Entities;

namespace Presentation.Entities.View;

public class UserForView
{
    public Guid Id { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public UserStatusForView UserStatus { get; set; } = null!;

}
