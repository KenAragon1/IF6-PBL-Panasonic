using System.ComponentModel.DataAnnotations;
using panasonic.Validations;

namespace panasonic.Dtos.User;

public class EditUserDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Employee ID is required.")]
    [UniqueEmployeeId]
    public int EmployeeID { get; set; }


    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress]
    [UniqueEmail]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Full Name is required.")]
    public string Fullname { get; set; } = string.Empty;

    [Required(ErrorMessage = "Role is required.")]
    public int RoleId { get; set; }
}