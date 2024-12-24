using System.Text.Json.Serialization;

namespace panasonic.Models;

public enum MaterialInventoryLocations
{
    PreperationRoom, ProductionLine
}

public class MaterialInventory
{
    public int Id { get; set; }
    public required int Quantity { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required MaterialInventoryLocations Location { get; set; }
    public required int MaterialId { get; set; }
    public Material? Material { get; set; }
    public int? ProductionLineId { get; set; }
    public ProductionLine? ProductionLine { get; set; }


}

