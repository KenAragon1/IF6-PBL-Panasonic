using System.ComponentModel.DataAnnotations;
using panasonic.Validations;

namespace panasonic.Dtos.UserDto;

public class UserRegisterDto
{
    [Required(ErrorMessage = "Employee ID is required.")]
    [RegularExpression(@"^\d{8}$", ErrorMessage = "Employee Id must be 8 digits")]
    [UniqueEmployeeId]
    public int EmployeeID { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [UniqueEmail]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Full Name is required.")]
    public string Fullname { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(int.MaxValue, MinimumLength = 8, ErrorMessage = "Password must be at least {2} characters long.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Confirm Password is required.")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; } = string.Empty;
}