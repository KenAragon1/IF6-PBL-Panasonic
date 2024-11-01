namespace panasonic.Models;

public class MaterialStock
{
    public int Id { get; set; }

    public int Quantity { get; set; }

    public int MaterialId { get; set; }
    public Material Material { get; set; }

    public int AreaId { get; set; }
    public Area Area { get; set; }
}