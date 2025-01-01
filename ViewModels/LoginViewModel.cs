using System.ComponentModel.DataAnnotations;
using panasonic.Dtos.UserDto;

namespace panasonic.ViewModels;

public class LoginViewModel
{
    public UserLoginDto UserLoginDto { get; set; } = new UserLoginDto();
}