using panasonic.Dtos.User;
using panasonic.Models;

namespace panasonic.ViewModels;

public class CreateUserViewModel
{
    public CreateUserDto CreateUserDto { get; set; } = new CreateUserDto();

    public List<Role> Roles;
}