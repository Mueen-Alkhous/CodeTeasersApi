using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProblemRepository
{
    private AppDbContext _context;

    public ProblemRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<List<Problem>> GetProblemsWithCategoryAsync()
    {
        return await _context.Problems.Include(p => p.Categories).ToListAsync();
    }

    public async Task<Problem?> GetProblemWithCategoryAsync(Guid id)
    {
        return await _context.Problems.Include(p => p.Categories).FirstOrDefaultAsync(p => p.Id == id);
    }

    public void AddCategoriesToProblem(Problem problem, List<Category> categories)
    {
        problem.Categories.AddRange(categories);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
