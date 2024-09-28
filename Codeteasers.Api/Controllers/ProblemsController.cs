using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Entities;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProblemsController : ControllerBase
    {
        private readonly ProblemRepository _repository;

        public ProblemsController(ProblemRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Problem>>> Get()
        {
            var problems = await _repository.GetProblemsWithCategoryAsync();
            return Ok(problems);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Problem?>> Get(Guid id)
        {
            var problem = await _repository.GetProblemWithCategoryAsync(id);
            return Ok(problem);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProblemForCreation problem)
        {
            var problemId = Guid.NewGuid();
            string descriptionFolder = @"D:\CodeTeasers\Problems\Descriptions";
            string descriptionFile = Path.Combine(descriptionFolder, $"{problemId}.md");
            await System.IO.File.WriteAllTextAsync(descriptionFile, problem.Description);

            string templateFolder = @"D:\CodeTeasers\Problems\Templates";
            string templateFile = Path.Combine(templateFolder, $"{problemId}");
            await System.IO.File.WriteAllTextAsync(templateFile, problem.Template);

            Problem newProblem = new Problem()
            {
                Id = problemId,
                Level = problem.Level,
                Score = problem.Score,
                Categories = problem.Categories,
            };

            return CreatedAtAction(nameof(Get), newProblem.Id, newProblem);


        }


    }
}
