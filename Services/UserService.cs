using Microsoft.EntityFrameworkCore;
using panasonic.Models;

namespace panasonic.Services;


public interface IUserService
{
    Task<User> GetUserByEmployeeID(int employeeId);
    Task<User> GetUserByEmail(string email);
    Task InsertUser(User user);
}
public class UserService : IUserService
{
    private readonly ApplicationDbContext _dbContext;
    public UserService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User> GetUserByEmployeeID(int employeeId)
    {
        var user = await _dbContext.Users.Where(u => u.EmployeeID == employeeId).Include(u => u.Role).FirstOrDefaultAsync();

        return user;

    }
    public async Task<User> GetUserByEmail(string email)
    {
        var user = await _dbContext.Users.Where(u => u.Email == email).FirstOrDefaultAsync();

        return user;
    }

    public async Task InsertUser(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }
}