namespace panasonic.Models;

public class Material
{
    public int Id { get; set; }
    public int Number { get; set; }
    public string Name { get; set; } = string.Empty;
    public string UnitMeasurement { get; set; } = string.Empty;
    public int DetailQuantity { get; set; }
    public string DetailMeasurement { get; set; } = string.Empty;
    public bool IsDeleted { get; set; } = false;

}