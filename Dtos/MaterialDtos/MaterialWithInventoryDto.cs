using System.Net.NetworkInformation;
using panasonic.Models;

namespace panasonic.Dtos.MaterialDtos;

public class MaterialWithInventoriesDto
{
    public int MaterialId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Barcode { get; set; } = string.Empty;
    public List<MaterialInventory>? Inventories { get; set; } = new List<MaterialInventory>();
}

public class Inventory
{
    public int InventoryId { get; set; }
    public int Quantity { get; set; }
    public MaterialInventoryLocations Location { get; set; }
    public int? LineRemark { get; set; }
}