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
        return await _context.Users.Include(u => u.UserStatus).ToListAsync();
    }

    public async Task<User?> GetUserWithStatusAsync(Guid id)
    {
        return await _context.Users.Include(u => u.UserStatus).FirstOrDefaultAsync(u => u.Id == id);
    }

    public void AddUser(User user)
    {
        _context.Users.Add(user);
    }

    public void UpdateUser(User user) 
    {
        _context.Users.Update(user);
    }

    public void DeleteUser(User user)
    {
        user.IsDeleted = true;
        user.UserStatus.IsDeleted = true;

    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
