namespace panasonic.Models;

public enum TransactionTypes
{
    Send, Production, Pickup, Return
}

public class MaterialTransaction
{
    public int Id { get; set; }
    public required TransactionTypes Type { get; set; }
    public DateTime CreatedAt { get; set; }
    public required int UserId { get; set; }
    public User? User { get; set; }
    public int? ProductionLineId { get; set; }
    public ProductionLine? ProductionLine { get; set; }
    public List<MaterialTransactionDetail> MaterialTransactionDetails { get; set; } = new List<MaterialTransactionDetail>();

}