using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class User : BaseEntity
{

    [NotMapped]
    public UserStatus UserStatus { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string Username { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;

    [Required]
    [MinLength(6)]
    [MaxLength(50)]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    public ICollection<Submission> Submissions { get; set; } = [];

    public User()
    {
        var userStatus = new UserStatus();
    }
}
