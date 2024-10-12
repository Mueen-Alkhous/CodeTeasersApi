using Domain.Entities;
using Infrastructure.Repositories;
using Presentation.Entities.Creation;

namespace Presentation.Services;

public class ProblemService
{
    private readonly ProblemRepository _problemRepository;
    private readonly CategoryRepository _categoryRepository;



    public ProblemService(ProblemRepository repositoryRepository, CategoryRepository categoryRepository)
    {
        _problemRepository = repositoryRepository;
        _categoryRepository = categoryRepository;
    }

    public List<string> CheckValidation(ProblemForCreation problem)
    {
        var description = problem.Description;
        var template = problem.Template;
        var test = problem.Test;

        var errorMessages = new List<string>();
        if (description == null || description.Length == 0)
            errorMessages.Add("No description file was uploaded");
        if (test == null || test.Length == 0)
            errorMessages.Add("No test file was uploaded");
        if (template == null || template.Length == 0)
            errorMessages.Add("No template file was uploaded");

        if (errorMessages.Count > 0)
            return errorMessages;

        var descriptionExtension = Path.GetExtension(description!.FileName).ToLowerInvariant();
        var templateExtension = Path.GetExtension(template!.FileName).ToLowerInvariant();
        var testExtension = Path.GetExtension(test!.FileName).ToLowerInvariant();

        if (descriptionExtension != ".md")
            errorMessages.Add("Invalid description file extention.");
        if (templateExtension != ".py")
            errorMessages.Add("Invalid template file extention.");
        if (testExtension != ".py")
            errorMessages.Add("Invalid test file extention.");

        return errorMessages;
    }

    public async Task<Problem> CreateProblemAsync(ProblemForCreation problem)
    {
        List<Category> categories = [];
        foreach (string categoryTitle in  problem.Categories)
        {
            categories.Add((await _categoryRepository.GetByTitleAsync(categoryTitle))!);
        }

        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

        var newProblem = new Problem(
            problem.Title, 
            problem.Score, 
            problem.Level, 
            categories, 
            path);

        using (var stream = new FileStream(newProblem.DescriptionPath, FileMode.Create))
        {
            await problem.Description.CopyToAsync(stream);
        }
        using (var stream = new FileStream(newProblem.TemplatePath, FileMode.Create))
        {
            await problem.Template.CopyToAsync(stream);
        }
        using (var stream = new FileStream(newProblem.TestPath, FileMode.Create))
        {
            await problem.Test.CopyToAsync(stream);
        }

        return newProblem;
    }

    public async Task  UpdateProblemAsync(Guid id, ProblemForCreation problem)
    {
        var problemToUpdate = await _problemRepository.GetByIdAsync(id);

        problemToUpdate!.Title = problem.Title;
        problemToUpdate.Score = problem.Score;
        problemToUpdate.Level = problem.Level;

        using (var stream = new FileStream(problemToUpdate.DescriptionPath, FileMode.Create))
        {
            await problem.Description.CopyToAsync(stream);
        }
        using (var stream = new FileStream(problemToUpdate.TemplatePath, FileMode.Create))
        {
            await problem.Template.CopyToAsync(stream);
        }
        using (var stream = new FileStream(problemToUpdate.TestPath, FileMode.Create))
        {
            await problem.Test.CopyToAsync(stream);
        }

        _problemRepository.Update(problemToUpdate);
    }

}
