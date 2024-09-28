using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Submission : BaseEntity
{

    [Required]
    [ForeignKey("User")]
    public Guid UserId { get; set; }

    [Required]
    [ForeignKey("Problem")]
    public Guid ProblemId { get; set; }

    [Required]
    public int Score { get; set; }

    [Required]
    public int PassedTests { get; set; }
}
