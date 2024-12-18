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
    public DbSet<MaterialInventory> MaterialInventories { get; set; }
    public DbSet<MaterialTransaction> MaterialTransactions { get; set; }
    public DbSet<MaterialTransactionDetail> MaterialTransactionDetails { get; set; }

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
        modelBuilder.Entity<MaterialRequest>().Property(mr => mr.CreatedAt).HasDefaultValueSql("GETDATE()");
        modelBuilder.Entity<MaterialRequest>().Property(mr => mr.Status).HasConversion<string>().HasDefaultValue(MaterialRequestStatus.Pending);
        modelBuilder.Entity<MaterialRequest>().ToTable(tb => tb.HasCheckConstraint("CK_MaterialRequest_MaterialRequestStatus", "Status IN ('Pending', 'Verified', 'Approved', 'Rejected', 'Completed')"));

        // MaterialInventory
        modelBuilder.Entity<MaterialInventory>().Property(mi => mi.Location).HasConversion<string>();
        modelBuilder.Entity<MaterialInventory>().ToTable(tb => tb.HasCheckConstraint("CK_MaterialInventory_MaterialInventoryLocation", "Location IN ('PreperationRoom', 'ProductionLine')"));

        // Material Transaction
        modelBuilder.Entity<MaterialTransaction>().Property(mt => mt.CreatedAt).HasDefaultValueSql("GETDATE()");
        modelBuilder.Entity<MaterialTransaction>().Property(mt => mt.Type).HasConversion<string>();
        modelBuilder.Entity<MaterialTransaction>().ToTable(tb => tb.HasCheckConstraint("CK_MaterialTransaction_MaterialTransactionType", "Type IN ('Send', 'Production', 'Return', 'Pickup')"));
        modelBuilder.Entity<MaterialTransaction>().HasMany(mt => mt.MaterialTransactionDetails).WithOne(mtd => mtd.MaterialTransaction).HasForeignKey(mtd => mtd.TransactionId);

    }


}
