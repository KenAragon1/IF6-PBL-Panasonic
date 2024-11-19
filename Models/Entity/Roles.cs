namespace panasonic.Models;

public class Role
{
    public int Id { get; set; }
    public required string RoleName { get; set; }
    public required string DisplayName { get; set; }
    public List<User> Users { get; set; }
}