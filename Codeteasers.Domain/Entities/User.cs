using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class User : BaseEntity
{

    

    [Required]
    [MaxLength(50)]
    public string Username { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string NormalizedUsername { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;

    [Required]
    [MinLength(6)]
    [MaxLength(50)]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [NotMapped]
    public UserStatus UserStatus { get; set; } = null!;

    public ICollection<Submission> Submissions { get; set; } = [];

    public User()
    {
    }

    public User(string username, string email, string password)
    {
        Username = username;
        NormalizedUsername = NormalizeUsername(username);
        Email = email;
        Password = password;
        UserStatus = new UserStatus();
    }

    private static string NormalizeUsername(string username)
    {
        var normalizedTitle = username.ToLower().Replace(" ", "_").Trim();
        return normalizedTitle;
    }
}
