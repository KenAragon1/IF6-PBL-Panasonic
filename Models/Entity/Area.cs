namespace panasonic.Models;

public class Area
{
    public int Id { get; set; }
    public string? Specifier { get; set; }
    public int AreaTypeId { get; set; }
    public AreaType AreaType { get; set; }
    public List<User> Users { get; set; }
    public List<AreaMaterial> AreaMaterials { get; set; }
}