using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Entities.Creation
{
    public class ProblemForCreation
    {
        [Required]
        public required string Title { get; set; }
        [Required]
        public required int Score { get; set; }
        [Required]
        public required string Level { get; set; }
        [Required]
        public required string Description { get; set; }
        [Required]
        public required string Template { get; set; }
        [Required]
        public required string Test { get; set; }
        [Required]
        public required List<Category> Categories { get; set; }

    }
}
