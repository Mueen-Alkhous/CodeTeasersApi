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
        return await _context.Categories.Where(c => c.IsDeleted == false).ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(Guid id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category.IsDeleted == false)
            return category;
        return null;
    }

    public void AddCategory(Category category)
    {
        _context.Categories.Add(category);
    }

    public void DeleteCategory(Category category)
    {
        category.IsDeleted = true;
        
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
