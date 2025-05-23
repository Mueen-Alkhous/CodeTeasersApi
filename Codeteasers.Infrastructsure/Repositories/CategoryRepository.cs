﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CategoryRepository
{
    private readonly AppDbContext _context; // Inject DbContext
    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Asynchronously return all categories
    /// </summary>
    /// <returns>List<Category></returns>
    public async Task<List<Category>> GetAllAsync()
    {
        return await _context.Categories
            .Include(c => c.Problems)
            .ToListAsync();
    }


    /// <summary>
    /// Asynchronously return a category by its id
    /// </summary>
    /// <param name="id">The id of the category to be returned</param>
    /// <returns>Category if found, and null if not</returns>
    public async Task<Category?> GetByIdAsync(Guid id)
    {
        return await _context.Categories.FindAsync(id);
    }

    /// <summary>
    /// Asynchronously return a category by its normalized title
    /// </summary>
    /// <param name="title">The normalized title of the category to be returned</param>
    /// <returns>Category if found, and null if not</returns>
    public async Task<Category?> GetByTitleAsync(string title)
    {
        return await _context.Categories.FirstOrDefaultAsync(c => c.NormalizedTitle == title);
    }

    /// <summary>
    /// Add a catetory to the database
    /// </summary>
    /// <param name="category">The category to be added</param>
    public void Add(Category category)
    {
        _context.Categories.Add(category);
    }

    /// <summary>
    /// Remove a category from the database
    /// </summary>
    /// <param name="category">The category to be deleted</param>
    public void Delete(Category category)
    {
        _context.Categories.Remove(category);
        
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
