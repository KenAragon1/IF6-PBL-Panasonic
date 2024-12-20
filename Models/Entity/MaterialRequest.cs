namespace panasonic.Models;

public enum MaterialRequestStatus
{
    Pending, Verified, Approved, Rejected, Completed
}

public class MaterialRequest
{
    public int Id { get; set; }
    public required int RequestedQuantity { get; set; }
    public int FullfilledQuantity { get; set; }
    public DateTime RequiredAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public MaterialRequestStatus Status { get; set; }
    public required int MaterialId { get; set; }
    public Material? Material { get; set; }
    public required int ProductionLineId { get; set; }
    public ProductionLine? ProductionLine { get; set; }
    public required int RequestedById { get; set; }
    public User? RequestedBy { get; set; }
    public int? VerifiedById { get; set; }
    public User? VerifiedBy { get; set; }
    public DateTime? VerifiedAt { get; set; }
    public int? ApprovedById { get; set; }
    public User? ApprovedBy { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public int? RejectedById { get; set; }
    public User? RejectedBy { get; set; }

    public void Verify(int verifiedById)
    {
        Status = MaterialRequestStatus.Verified;
        VerifiedAt = DateTime.Now;
        VerifiedById = verifiedById;
    }

    public void Reject(int rejectedById)
    {
        Status = MaterialRequestStatus.Rejected;
        RejectedById = rejectedById;
    }

    public void Approve(int approvedById)
    {
        Status = MaterialRequestStatus.Approved;
        ApprovedById = approvedById;
    }


}