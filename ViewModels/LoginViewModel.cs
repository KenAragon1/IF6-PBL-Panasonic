using System.ComponentModel.DataAnnotations;

namespace panasonic.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Employee ID Required")]
    public int EmployeeID { get; set; }

    [Required(ErrorMessage = "Password Required")]
    [DataType(DataType.Password)]
    public required string Password { get; set; }
}