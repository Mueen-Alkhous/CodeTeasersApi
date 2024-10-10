using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<UserStatus> UserStatuses { get; set; }
    public DbSet<Problem> Problems { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Submission> Submissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure one-to-one relationship between `User` and `UserStatus`
        modelBuilder.Entity<User>()
                    .HasOne(u => u.UserStatus)
                    .WithOne(us => us.User)
                    .HasForeignKey<UserStatus>(us => us.UserId);

        // Make the title of problems unique
        modelBuilder.Entity<Problem>()
                    .HasIndex(p => p.Title)
                    .IsUnique();

        // Make the username of users unique
        modelBuilder.Entity<User>()
                    .HasIndex(p => p.Username)
                    .IsUnique();

        // Make the email of users unique
        modelBuilder.Entity<User>()
                    .HasIndex(p => p.Email)
                    .IsUnique();
    }
}
