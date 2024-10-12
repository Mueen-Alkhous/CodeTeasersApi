using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Category : BaseEntity
{

    [Required]
    [MaxLength(50)]
    public string Title { get; set; } = null!;

    public string NormalizedTitle { get; set; }

    public List<Problem> Problems { get; set; } = [];

    public Category(string title)
    {
        Title = title;
        NormalizedTitle = NormalizeTitle(Title);
    }

    private string NormalizeTitle(string title)
    {
        var normalizedTitle = title.ToLower().Replace(" ", "_").Trim();
        return normalizedTitle;
    }
}
