using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Category : BaseEntity
{

    [Required]
    [MaxLength(50)]
    public string Title { get; set; } = null!;

    public List<Problem> Problems { get; set; } = [];

}
