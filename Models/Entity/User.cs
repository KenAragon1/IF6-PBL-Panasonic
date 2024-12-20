

namespace panasonic.Models;

public enum UserRoles
{
    Guest = 0, ShiftLeader, AsistantLeader, StoreManager, Admin, MaterialHandler
}

public class User
{
    public int Id { get; set; }
    public int EmployeeID { get; set; }
    public required string Fullname { get; set; }
    public required string Email { get; set; }
    public string HashedPassword { get; set; } = string.Empty;
    public bool IsVerified { get; set; } = false;
    public bool IsDeleted { get; set; } = false;
    public UserRoles Role { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? RecoveryToken { get; set; }
    public DateTime? TokenExpiry { get; set; }
}