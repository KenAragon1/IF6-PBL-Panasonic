namespace panasonic.Models;

public enum MaterialInventoryLocations
{
    Store = 0, PreperationRoom, ProductionLine
}

public class MaterialInventory
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public MaterialInventoryLocations Location { get; set; }
    public int MaterialId { get; set; }
    public Material Material { get; set; }
    public int? ProductionLineId { get; set; }
    public ProductionLine? ProductionLine { get; set; }
    public int? StagingProductionLineId { get; set; }
    public ProductionLine? StagingProductionLine { get; set; }

}

