using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Entities.Creation;
using Presentation.Entities.View;
using Presentation.Services;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProblemsController : ControllerBase
    {
        private readonly ProblemRepository _repository;
        private readonly IMapper _mapper;
        private readonly ProblemService _service;

        public ProblemsController(ProblemRepository repository, IMapper mapper, ProblemService service)
        {
            _repository = repository;
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Problem>>> Get()
        {
            var problems = await _repository.GetProblemsWithCategoryAsync();
            var problemsToReture = _mapper.Map<ProblemForView>(problems);
            return Ok(problemsToReture);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Problem>> Get(Guid id)
        {
            var problem = await _repository.GetProblemWithCategoryAsync(id);

            if (problem == null)
                return NotFound();

            var problemToReturn = _mapper.Map<ProblemForView>(problem);
            return Ok(problemToReturn);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProblemForCreation problem)
        {
            var newProblem = await _service.CreateProblemAsync(problem);

            var problemToReturn = _mapper.Map<ProblemForView>(newProblem);

            return CreatedAtAction(nameof(Get), new { id = newProblem.Id }, problemToReturn);

        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] ProblemForCreation problem)
        {
            var problemToUpdate = await _repository.GetProblemWithCategoryAsync(id);

            if (problemToUpdate == null)
                return NotFound();

            await _service.UpdateProblemAsync(id, problem);

            await _repository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var problem = await _repository.GetProblemWithCategoryAsync(id);

            if (problem == null)
                return NotFound();

            problem.IsDeleted = true;

            foreach (var submission in problem.Submissions)
                submission.IsDeleted = true;

            await _repository.SaveChangesAsync();

            return NoContent();
        }


    }
}
