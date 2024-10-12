using Microsoft.EntityFrameworkCore;

namespace panasonic.Models;

public class User
{
    public int Id { get; set; }
    public int EmployeeID { get; set; }

    public required string Fullname { get; set; }

    public required string Email { get; set; }

    public string HashedPassword { get; set; }

    public int RoleId { get; set; }

    public Role Role { get; set; }
}