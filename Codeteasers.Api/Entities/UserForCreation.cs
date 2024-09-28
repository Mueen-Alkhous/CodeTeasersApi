using System.ComponentModel.DataAnnotations;

namespace Web.Api.Entities;

public class UserForCreation
{
    [Required]
    [StringLength(50)]
    public required string Username { get; set; }

    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 6)]
    public required string Password { get; set; }
}
