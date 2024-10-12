using Microsoft.EntityFrameworkCore;
using panasonic.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        modelBuilder.Entity<User>().HasIndex(u => u.EmployeeID).IsUnique();

        modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, RoleName = "unverified" },
            new Role { Id = 2, RoleName = "admin" }
        );


    }
}
