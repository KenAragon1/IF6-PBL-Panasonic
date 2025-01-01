using System.ComponentModel.DataAnnotations;

namespace panasonic.Dtos.UserDto;

public class UserLoginDto
{
    [Required(ErrorMessage = "Employee ID Required")]
    public int EmployeeID { get; set; }

    [Required(ErrorMessage = "Password Required")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
}