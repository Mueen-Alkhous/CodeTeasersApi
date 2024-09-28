using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<UserStatus> UserStatuses { get; set; }
    public DbSet<Problem> Problems { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Submission> Submissions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
                    .HasOne(u => u.UserStatus)
                    .WithOne(us => us.User)
                    .HasForeignKey<UserStatus>(us => us.UserId);
    }
}
