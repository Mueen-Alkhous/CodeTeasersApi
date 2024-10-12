using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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

            var problems = await _repository.GetAllAsync();
            var problemsToReture = _mapper.Map<List<ProblemForView>>(problems);
            return Ok(problemsToReture);
        }

        

        [HttpGet("{title}")]
        public async Task<ActionResult<Problem>> Get(string title)
        {
            var problem = await _repository.GetByTitleAsync(title);

            if (problem == null)
                return NotFound();

            var problemToReturn = _mapper.Map<ProblemForView>(problem);

            return Ok(problemToReturn);
        }

        [HttpGet("{title}/downloadFile/{fileType}")]
        public async Task<IActionResult> DownloadFile(string title, string fileType)
        {
            var problem = await _repository.GetByTitleAsync(title);

            if (problem == null)
                return NotFound();

            if (fileType == null)
                return BadRequest();

            else if (fileType == "description")
                return PhysicalFile(problem.DescriptionPath, "text/markdown", Path.GetFileName(problem.DescriptionPath));

            else if (fileType == "template")
                return PhysicalFile(problem.TemplatePath, "text/x-python", Path.GetFileName(problem.TemplatePath));
            else
                return PhysicalFile(problem.TestPath, "text/x-python", Path.GetFileName(problem.TestPath));
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] ProblemForCreation problem)
        {
            var checkProblemValid = _service.CheckValidation(problem);

            if (checkProblemValid.Count > 0)
                return BadRequest(checkProblemValid);

            var newProblem = await _service.CreateProblemAsync(problem);

            _repository.Add(newProblem);

            await _repository.SaveChangesAsync();

            var problemToReturn = _mapper.Map<ProblemForView>(newProblem);

            var descriptionUrl = Url.Action("DownloadFile", new { normalizedTitle = newProblem.NormalizedTitle, fileType = "description" });

            var templateUrl = Url.Action("DownloadFile", new { normalizedTitle = newProblem.NormalizedTitle, fileType = "template" });

            var testUrl = Url.Action("DownloadFile", new { normalizedTitle = newProblem.NormalizedTitle, fileType = "test" });

            problemToReturn.DescriptionUrl = descriptionUrl!;
            problemToReturn.TemplateUrl = templateUrl!;
            problemToReturn.TestUrl = testUrl!;


            return CreatedAtAction(nameof(Get), new { normalizedTitle = newProblem.NormalizedTitle }, problemToReturn);
        }

        [HttpPut("{title}")]
        public async Task<ActionResult> Put(string title, [FromBody] ProblemForCreation problem)
        {
            var problemToUpdate = await _repository.GetByTitleAsync(title);

            if (problemToUpdate == null)
                return NotFound();

            await _service.UpdateProblemAsync(problemToUpdate.Id, problem);

            await _repository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{title}")]
        public async Task<ActionResult> Patch(string title, [FromBody] JsonPatchDocument<Problem> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest();

            var problem = await _repository.GetByTitleAsync(title);
            if (problem == null)
                return NotFound();

            patchDoc.ApplyTo(problem, ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _repository.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{title}")]
        public async Task<ActionResult> Delete(string title)
        {
            var problem = await _repository.GetByTitleAsync(title);

            if (problem == null) 
                return NotFound();

            _repository.Delete(problem);

            await _repository.SaveChangesAsync();

            return NoContent();
        }


    }
}
