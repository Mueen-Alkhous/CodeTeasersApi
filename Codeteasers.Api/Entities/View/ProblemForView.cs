using Domain.Entities;
using Domain.Enums;

namespace Presentation.Entities.View;

public class ProblemForView
{
    public string Title { get; set; } = string.Empty;
    public string NormalizedTitle { get; set; } = string.Empty;
    public string Level { get; set; } = string.Empty;
    public int Score { get; set; }
    public string DescriptionUrl { get; set; } = string.Empty ;
    public string TemplateUrl { get; set; } = string.Empty ;
    public string TestUrl { get; set; } = string.Empty;
    public List<CategoryForView> Categories { get; set; } = [];
}
