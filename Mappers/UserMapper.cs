using panasonic.Dtos.User;
using panasonic.Models;

namespace panasonic.Mappers;

public static class UserMapper
{
    public static UserDto ToUserDto(this User user)
    {
        return new UserDto { Id = user.Id, EmployeeID = user.EmployeeID, Email = user.Email, Fullname = user.Fullname, Role = user.Role, CreatedAt = user.CreatedAt };
    }
}