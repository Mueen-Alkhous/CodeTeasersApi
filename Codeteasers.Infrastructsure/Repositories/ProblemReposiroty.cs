using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProblemRepository
{
    private readonly AppDbContext _context; // Inject DbContext

    public ProblemRepository(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Asynchronously return a list of problems with thier categories
    /// </summary>
    /// <returns></returns>
    public async Task<List<Problem>> GetProblemsWithCategoryAsync()
    {
        return await _context.Problems
            .Include(p => p.Categories)
            .ToListAsync();
    }

    /// <summary>
    /// Asynchronously return a problem with its categories
    /// </summary>
    /// <param name="id">The id of the problem to be returned</param>
    /// <returns>Problem with its categories</returns>
    public async Task<Problem?> GetProblemWithCategoryAsync(Guid id)
    {
        return await _context.Problems
            .Include(p => p.Categories)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    /// <summary>
    /// Add a problem to the database
    /// </summary>
    /// <param name="problem">The problem to be added</param>
    public void AddProblem(Problem problem) 
    {
        _context.Problems.Add(problem);
    }

    /// <summary>
    /// Add a list of categories to the problem
    /// </summary>
    /// <param name="problem">The problem to be the categories added to</param>
    /// <param name="categories">List of categories to be added</param>
    public void AddCategoriesToProblem(Problem problem, List<Category> categories)
    {
        problem.Categories.AddRange(categories);
    }

    /// <summary>
    /// Update a problem in the database
    /// </summary>
    /// <param name="problem">The problem to be updated</param>
    public void UpdateProblem(Problem problem)
    {
        _context.Problems.Update(problem);
    }

    /// <summary>
    /// Remove a problem from the database
    /// </summary>
    /// <param name="problem">The problem to be removed</param>
    public void DeleteProblem(Problem problem)
    {
        _context.Problems.Remove(problem);
    }

    /// <summary>
    /// Save the changes to the database asynchronously
    /// </summary>
    /// <returns></returns>
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
