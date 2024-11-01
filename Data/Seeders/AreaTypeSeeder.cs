using Microsoft.EntityFrameworkCore;
using panasonic.Models;

namespace panasonic.Seeders;

public class AreaTypeSeeder
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AreaType>().HasData(
            new AreaType { Id = 1, Type = "Storage", Label = "Storage" },
            new AreaType { Id = 2, Type = "PreperationRoom", Label = "Preperation Room" },
            new AreaType { Id = 3, Type = "ProductionLine", Label = "Production Line" }
        );
    }
}