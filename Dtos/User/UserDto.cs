using panasonic.Models;

namespace panasonic.Dtos.User;

public class UserDto
{
    public int Id { get; set; }
    public int EmployeeID { get; set; }

    public string Fullname { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public int RoleId { get; set; }
    public Role Role { get; set; }

    public DateTime CreatedAt { get; set; }
}