using Domain.Entities;

namespace Presentation.Entities.View;

public class ProblemForView
{
    public string Title { get; set; } = string.Empty;
    public string Level { get; set; } = string.Empty;
    public int Score { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Template { get; set; } = string.Empty;
    public List<Category> Categories { get; set; } = [];
}
