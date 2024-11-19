using System.ComponentModel.DataAnnotations;
using panasonic.Validations;


namespace panasonic.ViewModels;
public class RegisterViewModel
{
    [Required(ErrorMessage = "Employee ID is required.")]
    // [Range(8, 8, ErrorMessage = "Employee Id must be 8 digits")]
    [UniqueEmployeeId]
    public int EmployeeID { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [UniqueEmail]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Full Name is required.")]
    public required string Fullname { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(100, ErrorMessage = "Password must be at least {2} characters long.", MinimumLength = 8)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
    public required string Password { get; set; }

    [Required(ErrorMessage = "Confirm Password is required.")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public required string ConfirmPassword { get; set; }
}