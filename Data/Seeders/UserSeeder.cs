using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using panasonic.Models;

namespace panasonic.Seeders;

public class UserSeeder
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        var _passwordHasher = new PasswordHasher<User>();

        var admin = new User { Id = 1, EmployeeID = 301010, Fullname = "Admin", Email = "admin@email.com", Role = UserRoles.Admin, IsVerified = true };
        var asistantLeader = new User { Id = 2, EmployeeID = 301011, Fullname = "Asistant Leader", Email = "asistantleader@email.com", Role = UserRoles.AsistantLeader, IsVerified = true };
        var shiftLeader = new User { Id = 3, EmployeeID = 301012, Fullname = "Shift Leader", Email = "shiftleader@email.com", Role = UserRoles.ShiftLeader, IsVerified = true };
        var storeManager = new User { Id = 4, EmployeeID = 301013, Fullname = "Store Manager", Email = "storemanager@email.com", Role = UserRoles.StoreManager, IsVerified = true };

        admin.HashedPassword = _passwordHasher.HashPassword(admin, "password123");
        asistantLeader.HashedPassword = _passwordHasher.HashPassword(asistantLeader, "password123");
        shiftLeader.HashedPassword = _passwordHasher.HashPassword(shiftLeader, "password123");
        storeManager.HashedPassword = _passwordHasher.HashPassword(storeManager, "password123");

        modelBuilder.Entity<User>().HasData(
            admin, asistantLeader, shiftLeader, storeManager
        );
    }
}