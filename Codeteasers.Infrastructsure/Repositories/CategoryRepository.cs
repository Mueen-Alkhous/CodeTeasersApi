using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CategoryRepository
{
    private readonly AppDbContext _context;
    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Category>> GetAllAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(Guid id)
    {
        return await _context.Categories.FindAsync(id);
    }

    public void AddCategory(Category category)
    {
        _context.Categories.Add(category);
    }

    public void DeleteCategory(Category category)
    {
        _context.Categories.Remove(category);
        
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
