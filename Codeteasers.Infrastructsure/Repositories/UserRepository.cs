using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }


    /// <summary>
    /// Asynchronously return all users with thier status 
    /// </summary>
    /// <returns>List of users</returns>
    public async Task<List<User>> GetUsersWithStatusAsync()
    {
        return await _context.Users
            .Include(u => u.UserStatus)
            .ToListAsync();
    }

    /// <summary>
    /// Asynchronously return a user with status
    /// </summary>
    /// <param name="id"></param>
    /// <returns>User with status</returns>
    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.Users
            .Include(u => u.UserStatus)
            .FirstOrDefaultAsync(u => u.Id == id);
    }
    
    /// <summary>
    /// Asyncronously return a user by its normalized username
    /// </summary>
    /// <param name="normalizedUsername"></param>
    /// <returns>User with its status</returns>
    public async Task<User?> GetByUsernameAsync(string normalizedUsername)
    {
        return await _context.Users
            .Include(u => u.UserStatus)
            .FirstOrDefaultAsync(u => u.NormalizedUsername == normalizedUsername);
    }

    public void AddUser(User user)
    {
        _context.Users.Add(user);
    }

    public async Task<bool> DoesUserExistAsync(string username)
    {
        if (await _context.Users.AnyAsync(u => u.Username == username))
            return true;
        return false;

    }

    public async Task<bool> DoesEmailExistAsync(string email)
    {
        if (await _context.Users.AnyAsync(u => u.Email == email))
            return true;
        return false;
    }

    public void Update(User user) 
    {
        _context.Users.Update(user);
    }

    public void Delete(User user)
    {
        _context.Users.Remove(user);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
