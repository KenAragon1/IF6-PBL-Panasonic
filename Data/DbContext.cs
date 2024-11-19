using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using panasonic.Models;
using panasonic.Seeders;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Material> Materials { get; set; }
    public DbSet<AreaType> AreaTypes { get; set; }
    public DbSet<Area> Areas { get; set; }
    public DbSet<AreaMaterial> AreaMaterials { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User Role
        RoleSeeder.Seed(modelBuilder);

        // User
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        modelBuilder.Entity<User>().HasIndex(u => u.EmployeeID).IsUnique();
        modelBuilder.Entity<User>().Property(u => u.RoleId).HasDefaultValue(1);
        modelBuilder.Entity<User>().Property(u => u.CreatedAt).HasDefaultValueSql("GETDATE()");

        UserSeeder.Seed(modelBuilder);

        // Area Type
        AreaTypeSeeder.Seed(modelBuilder);

        // Area
        modelBuilder.Entity<Area>().HasIndex(a => a.Specifier).IsUnique();
        modelBuilder.Entity<Area>().HasData(
            new Area { Id = 1, AreaTypeId = 1 }
        );


    }
}
