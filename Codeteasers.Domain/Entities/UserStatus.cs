using Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class UserStatus : BaseEntity
{

    [Required]
    public int Score { get; set; }

    [Required]
    public int Submissions { get; set; }

    [Required]
    public int PerfectSubmissions { get; set; }

    [Required]
    public string Rank { get; set; } = Ranks.Novice.ToString();

    [Required]
    [ForeignKey("User")]
    public Guid UserId { get; set; }

    [Required]
    [NotMapped]
    public User User { get; set; } = null!;
}
