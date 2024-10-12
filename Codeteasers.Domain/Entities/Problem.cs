using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Problem : BaseEntity
{

    [Required, MaxLength(100)]
    public string Title { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string NormalizedTitle {  get; set; }

    [Required]
    public string DescriptionPath { get; set; }

    [Required]
    public string DescriptionUrl { get; set; }

    [Required]
    public string TestPath { get; set; }

    [Required]
    public string TestUrl { get; set; }

    [Required]
    public string TemplatePath { get; set; }

    [Required]
    public string TemplateUrl { get; set; }

    [Required]
    public string Level { get; set; } = string.Empty;

    [Required]
    public int Score { get; set; }

    public List<Submission> Submissions { get; set; } = [];

    public List<Category> Categories { get; set; } = [];

    public Problem()
    {
        NormalizedTitle = NormalizeTitle(Title);

        DescriptionPath = $@"D:\CodeTeasers\Problems\Descriptions\{NormalizedTitle}_description.md";

        TestPath = $@"D:\CodeTeasers\Problems\Tests\{NormalizedTitle}_test.py";

        TemplatePath = $@"D:\CodeTeasers\Problems\Templates\{NormalizedTitle}_template.py";

        DescriptionUrl = $"/api/Problems/{NormalizedTitle}/downloadFile/description";
        TestUrl = $"/api/Problems/{NormalizedTitle}/downloadFile/test";
        TemplateUrl = $"/api/Problems/{NormalizedTitle}/downloadFile/template";
    }

    public Problem(string title, int score, string level, List<Category> categories)
    {
        this.Title = title; 
        this.Score = score;
        this.Level = level;
        this.Categories = categories;
        NormalizedTitle = NormalizeTitle(Title);
        DescriptionPath = $@"D:\CodeTeasers\Problems\Descriptions\{NormalizedTitle}_description.md";
        TestPath = $@"D:\CodeTeasers\Problems\Tests\{NormalizedTitle}_test.py";
        TemplatePath = $@"D:\CodeTeasers\Problems\Templates\{NormalizedTitle}_template.py";
        DescriptionUrl = $"/api/Problems/{NormalizedTitle}/downloadFile/description";
        TestUrl = $"/api/Problems/{NormalizedTitle}/downloadFile/test";
        TemplateUrl = $"/api/Problems/{NormalizedTitle}/downloadFile/template";
    }

    private string NormalizeTitle(string title)
    {
        var normalizedTitle = title.ToLower().Replace(" ", "_").Trim();
        return normalizedTitle;
    } 
}
