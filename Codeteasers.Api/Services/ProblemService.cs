using Domain.Entities;
using Infrastructure.Repositories;
using Presentation.Entities.Creation;

namespace Presentation.Services;

public class ProblemService
{
    private readonly ProblemRepository _repository;

    public ProblemService(ProblemRepository repository)
    {
        _repository = repository;
    }

    public async Task<Problem> CreateProblemAsync(ProblemForCreation problem)
    {
        Problem newProblem = new Problem()
        {
            Level = problem.Level,
            Score = problem.Score,
            Categories = problem.Categories,
        };

        await System.IO.File.WriteAllTextAsync(newProblem.DescriptionPath, problem.Description);
        await System.IO.File.WriteAllTextAsync(newProblem.TemplatePath, problem.Template);
        await System.IO.File.WriteAllTextAsync(newProblem.TestPath, problem.Test);

        return newProblem;
    }

    public async Task  UpdateProblemAsync(Guid id, ProblemForCreation problem)
    {
        var problemToUpdate = await _repository.GetProblemWithCategoryAsync(id);

        problemToUpdate!.Title = problem.Title;
        problemToUpdate.Score = problem.Score;
        problemToUpdate.Level = problem.Level;

        await System.IO.File.WriteAllTextAsync(problemToUpdate.DescriptionPath, problem.Description);
        await System.IO.File.WriteAllTextAsync(problemToUpdate.TemplatePath, problem.Template);
        await System.IO.File.WriteAllTextAsync(problemToUpdate.TestPath, problem.Test);

        _repository.UpdateProblem(problemToUpdate);
    }

}
