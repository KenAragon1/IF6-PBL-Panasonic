using System.ComponentModel.DataAnnotations;

namespace panasonic.Models;

public enum TransactionTypes
{
    Send, Production, Pickup, Return
}

public class MaterialTransaction
{
    public int Id { get; set; }
    public required int Quantity { get; set; }
    public required TransactionTypes Type { get; set; }
    public DateTime CreatedAt { get; set; }
    public required int MaterialId { get; set; }
    public Material? Material { get; set; }
    public int? ProductionLineId { get; set; }
    public ProductionLine? ProductionLine { get; set; }

}