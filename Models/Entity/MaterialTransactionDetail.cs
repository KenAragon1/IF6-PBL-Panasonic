namespace panasonic.Models;


public class MaterialTransactionDetail
{
    public int Id { get; set; }
    public int TransactionId { get; set; }
    public MaterialTransaction? MaterialTransaction { get; set; }
    public int MaterialId { get; set; }
    public Material? Material { get; set; }
    public int Quantity { get; set; }
}