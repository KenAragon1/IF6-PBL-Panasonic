using panasonic.Data.Queries;
using panasonic.Models;

namespace panasonic.ViewModels;

public class UserViewModel
{
    public required List<User> users { get; set; }
    public required List<Role> roles { get; set; }
    public UserQueryObject UserQueryObject { get; set; } = new UserQueryObject();
}