using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Problem : BaseEntity
{

    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string DescriptionPath { get; set; }

    [Required]
    public string TestPath { get; set; }

    [Required]
    public string TemplatePath { get; set; }

    public string Level { get; set; } = string.Empty;

    [Required]
    public int Score { get; set; }

    public List<Submission> Submissions { get; set; } = [];

    public List<Category> Categories { get; set; } = [];

    public Problem()
    {
        DescriptionPath = $@"D:\CodeTeasers\Problems\Descriptions\{Id}";
        TestPath = $@"D:\CodeTeasers\Problems\Tests\{Id}";
        TemplatePath = $@"D:\CodeTeasers\Problems\Templates\{Id}";
    }
}
