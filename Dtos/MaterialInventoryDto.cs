using panasonic.Models;

namespace panasonic.Dtos;

public class MaterialInventoryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int AvailableQuantity { get; set; }
    public required string Barcode { get; set; }
    public MaterialInventoryLocations Location { get; set; }
    public int? Remark { get; set; }
}

