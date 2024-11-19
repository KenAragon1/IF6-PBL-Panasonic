using System.ComponentModel.DataAnnotations;
using panasonic.Data.Queries;
using panasonic.Models;
using panasonic.Validations;

namespace panasonic.ViewModels.UserViewModel;

public class IndexViewModel
{
    public required List<User> users { get; set; }

    public required List<Role> roles { get; set; }
    public UserQueryObject UserQueryObject { get; set; } = new UserQueryObject();
}

public class CreateUserViewModel
{
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

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(100, ErrorMessage = "Password must be at least {2} characters long.", MinimumLength = 8)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
    ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
    public string Password { get; set; } = string.Empty;

    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; } = string.Empty;

    public List<Role>? roles { get; set; }
}

public class UserDetailViewModel
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

    public List<Role>? roles { get; set; }
}

