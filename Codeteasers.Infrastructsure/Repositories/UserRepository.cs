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

    public async Task<List<User>> GetUsersWithStatusAsync()
    {
        return await _context.Users
            .Include(u => u.UserStatus)
            .Where(u => u.IsDeleted == false)
            .ToListAsync();
    }

    public async Task<User?> GetUserWithStatusAsync(Guid id)
    {
        return await _context.Users
            .Include(u => u.UserStatus)
            .Where(u => u.IsDeleted == false)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async void AddUser(User user)
    {
        _context.Users.Add(user);
    }

    public async Task<bool> IsUsernameExistsAsync(string username)
    {
        if (await _context.Users.AnyAsync(u => u.Username == username))
            return true;
        return false;

    }

    public async Task<bool> IsEmailExistsAsync(string email)
    {
        if (await _context.Users.AnyAsync(u => u.Email == email))
            return true;
        return false;
    }

    public void UpdateUser(User user) 
    {
        _context.Users.Update(user);
    }

    public void DeleteUser(User user)
    {
        user.IsDeleted = true;
        user.UserStatus.IsDeleted = true;
        foreach(var submission in user.Submissions)
            submission.IsDeleted = true;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
