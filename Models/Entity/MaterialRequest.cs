namespace panasonic.Models;

public enum MaterialRequestStatus
{
    Pending, Verified, Approved, Rejected, Completed
}

public class MaterialRequest
{
    public int Id { get; set; }
    public required int Quantity { get; set; }
    public DateOnly RequestedAt { get; set; }
    public MaterialRequestStatus Status { get; set; }
    public required int MaterialId { get; set; }
    public Material? Material { get; set; }
    public required int DestinationId { get; set; }
    public Area? Destination { get; set; }
    public required int RequestedById { get; set; }
    public User? RequestedBy { get; set; }
    public int? VerifiedById { get; set; }
    public User? VerifiedBy { get; set; }
    public int? AprrovedById { get; set; }
    public User? ApprovedBy { get; set; }
    public int? RejectedById { get; set; }
    public User? RejectedBy { get; set; }


}