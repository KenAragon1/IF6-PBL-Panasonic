using Microsoft.EntityFrameworkCore;
using panasonic.Models;

namespace panasonic.Seeders;

public class RoleSeeder
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, RoleName = "Guest", DisplayName = "Guest" },
            new Role { Id = 2, RoleName = "Admin", DisplayName = "Admin" },
            new Role { Id = 3, RoleName = "StoreManager", DisplayName = "Store Manager" },
            new Role { Id = 4, RoleName = "ShiftLeader", DisplayName = "Shift Leader" },
            new Role { Id = 5, RoleName = "AsistantLeader", DisplayName = "Asistant Leader" }
        );
    }
}