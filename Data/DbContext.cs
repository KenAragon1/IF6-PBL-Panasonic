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
    public DbSet<Material> Materials { get; set; }
    public DbSet<ProductionLine> ProductionLines { get; set; }
    public DbSet<MaterialRequest> MaterialRequests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        // User
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        modelBuilder.Entity<User>().HasIndex(u => u.EmployeeID).IsUnique();
        modelBuilder.Entity<User>().Property(u => u.Role).HasDefaultValue(UserRoles.Guest).HasConversion<string>();
        modelBuilder.Entity<User>().ToTable(tb => tb.HasCheckConstraint("CK_User_UserRole", "Role IN ('ShiftLeader', 'AsistantLeader', 'StoreManager', 'Admin', 'MaterialHandler', 'Guest')"));
        modelBuilder.Entity<User>().Property(u => u.CreatedAt).HasDefaultValueSql("GETDATE()");

        UserSeeder.Seed(modelBuilder);

        // Material
        modelBuilder.Entity<Material>().HasIndex(m => m.Number).IsUnique();

        // Production Line
        modelBuilder.Entity<ProductionLine>().HasIndex(pl => pl.Remark).IsUnique();

        // MaterialRequest
        modelBuilder.Entity<MaterialRequest>().Property(mr => mr.RequestedAt).HasDefaultValueSql("GETDATE()");
        modelBuilder.Entity<MaterialRequest>().Property(mr => mr.Status).HasConversion<string>().HasDefaultValue(MaterialRequestStatus.Pending);
        modelBuilder.Entity<MaterialRequest>().ToTable(tb => tb.HasCheckConstraint("CK_MaterialRequest_MaterialRequestStatus", "Status IN ('Pending', 'Verified', 'Approved', 'Rejected', 'Completed')"));
    }


}
