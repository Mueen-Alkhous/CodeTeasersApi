using Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Problem : BaseEntity
{

    [Required, MaxLength(100)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string DescriptionPath { get; set; }

    [Required, NotMapped]
    public FileInfo Description { get; set; }

    [Required]
    public string TestPath { get; set; }

    [Required, NotMapped]
    public FileInfo Test { get; set; }

    [Required]
    public string TemplatePath { get; set; }

    [Required, NotMapped]
    public FileInfo Template { get; set; }

    [Required]
    public string Level { get; set; } = string.Empty;

    [Required]
    public int Score { get; set; }

    public List<Submission> Submissions { get; set; } = [];

    public List<Category> Categories { get; set; } = [];

    public Problem()
    {
        DescriptionPath = $@"D:\CodeTeasers\Problems\Descriptions\{Id}";
        Description = new FileInfo(DescriptionPath);

        TestPath = $@"D:\CodeTeasers\Problems\Tests\{Id}";
        Test = new FileInfo(TestPath);

        TemplatePath = $@"D:\CodeTeasers\Problems\Templates\{Id}";
        Template = new FileInfo(TemplatePath);
    }
}
