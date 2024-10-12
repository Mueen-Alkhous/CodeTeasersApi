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
        public required IFormFile Description { get; set; }
        [Required]
        public required IFormFile Template { get; set; }
        [Required]
        public required IFormFile Test { get; set; }

        public List<string> Categories { get; set; } = [];

    }
}
