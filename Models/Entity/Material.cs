namespace panasonic.Models;

public class Material
{
    public int Id { get; set; }

    public required string Name { get; set; }
    public required string TotalQuantity { get; set; }
    public required string TotalWeight { get; set; }
}