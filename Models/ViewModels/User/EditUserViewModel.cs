
using panasonic.Dtos.User;
using panasonic.Models;

namespace panasonic.ViewModels;

public class EditUserViewModel
{
    public EditUserDto EditUserDto { get; set; }
    public List<Role> Roles { get; set; }
}