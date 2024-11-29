namespace panasonic.Models;


public enum AreaTypes
{
    ProductionLine, PreperationRoom, Store
}
public class Area
{
    public int Id { get; set; }
    public int Remark { get; set; }
    public AreaTypes Type { get; set; }
    public List<User>? Users { get; set; }
    public List<AreaMaterial>? AreaMaterials { get; set; }
}