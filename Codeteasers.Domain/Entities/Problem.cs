using Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

    [NotMapped]
    public string RootPath { get; set; } = string.Empty;

    public List<Submission> Submissions { get; set; } = [];

    public List<Category> Categories { get; set; } = [];

    public Problem()
    {
        NormalizedTitle = NormalizeTitle(Title);

        DescriptionPath = Path.Combine(RootPath, "Descriptions", $"{NormalizedTitle}_description.md");
        TemplatePath = Path.Combine(RootPath, "Templates", $"{NormalizedTitle}_template.md");
        TestPath = Path.Combine(RootPath, "Tests", $"{NormalizedTitle}_test.md");

        DescriptionUrl = $"/api/Problems/{NormalizedTitle}/downloadFile/description";
        TestUrl = $"/api/Problems/{NormalizedTitle}/downloadFile/test";
        TemplateUrl = $"/api/Problems/{NormalizedTitle}/downloadFile/template";
    }

    public Problem(string title, int score, string level, List<Category> categories, string rootPath)
    {
        this.Title = title; 
        this.Score = score;
        this.Level = level;
        this.RootPath = rootPath;
        this.Categories = categories;
        NormalizedTitle = NormalizeTitle(Title);
        DescriptionPath = Path.Combine(rootPath, "Descriptions", $"{NormalizedTitle}_description.md");
        TemplatePath = Path.Combine(rootPath, "Templates", $"{NormalizedTitle}_template.md");
        TestPath = Path.Combine(rootPath, "Tests", $"{NormalizedTitle}_test.md");
        DescriptionUrl = $"/api/Problems/{NormalizedTitle}/downloadFile/description";
        TestUrl = $"/api/Problems/{NormalizedTitle}/downloadFile/test";
        TemplateUrl = $"/api/Problems/{NormalizedTitle}/downloadFile/template";
    }

    private static string NormalizeTitle(string title)
    {
        var normalizedTitle = title.ToLower().Replace(" ", "_").Trim();
        return normalizedTitle;
    } 
}
