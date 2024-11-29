using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using panasonic.Models;
using panasonic.Seeders;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Material> Materials { get; set; }
    public DbSet<Area> Areas { get; set; }
    public DbSet<AreaMaterial> AreaMaterials { get; set; }
    public DbSet<MaterialRequest> MaterialRequests { get; set; }

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

        // Area
        modelBuilder.Entity<Area>().HasIndex(a => new { a.Remark, a.Type }).IsUnique();
        modelBuilder.Entity<Area>().Property(a => a.Type).HasConversion<string>();
        modelBuilder.Entity<Area>().ToTable(tb => tb.HasCheckConstraint("CK_Area_AreaType", "Type IN ('PreperationRoom', 'ProductionLine', 'Store')"));
        modelBuilder.Entity<Area>().HasData(
            new Area { Id = 1, Type = AreaTypes.Store }
        );

        // MaterialRequest
        modelBuilder.Entity<MaterialRequest>().Property(mr => mr.RequestedAt).HasDefaultValueSql("GETDATE()");
        modelBuilder.Entity<MaterialRequest>().Property(mr => mr.Status).HasConversion<string>().HasDefaultValue(MaterialRequestStatus.Pending);
        modelBuilder.Entity<MaterialRequest>().ToTable(tb => tb.HasCheckConstraint("CK_MaterialRequest_MaterialRequestStatus", "Status IN ('Pending', 'Verified', 'Approved', 'Rejected', 'Completed')"));

    }


}
