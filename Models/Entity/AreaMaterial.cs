namespace panasonic.Models;

public class AreaMaterial
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public DateOnly ExpirationDate { get; set; }
    public int MaterialId { get; set; }
    public Material? Material { get; set; }
    public int AreaId { get; set; }
    public Area? Area { get; set; }
}