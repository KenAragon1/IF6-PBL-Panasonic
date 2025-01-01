using System.ComponentModel.DataAnnotations;
using panasonic.Dtos.UserDto;
using panasonic.Validations;


namespace panasonic.ViewModels;
public class RegisterViewModel
{
    public UserRegisterDto UserRegisterDto { get; set; } = new UserRegisterDto();
}