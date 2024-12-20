using System.Net.Quic;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using panasonic.Data.Queries;
using panasonic.Models;

namespace panasonic.Repositories;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync(UserQueryObject userQueryObject, bool? isVerified = null);
    Task<User> GetAsync(int? id = null, int? employeeid = null, string? email = null);
    Task StoreAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(User user);
}

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;
    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<User>> GetAllAsync(UserQueryObject? userQueryObject = null, bool? isVerified = null)
    {
        var query = _dbContext.Users.OrderBy(u => u.Fullname).AsQueryable();

        if (userQueryObject != null)
        {
            if (userQueryObject.EmployeeID.HasValue || userQueryObject.EmployeeID != 0) query = query.Where(u => EF.Functions.Like(u.EmployeeID.ToString(), userQueryObject.EmployeeID + "%"));
            if (!string.IsNullOrEmpty(userQueryObject.Fullname)) query = query.Where(u => EF.Functions.Like(u.Fullname, userQueryObject.Fullname + "%"));
            if (!string.IsNullOrEmpty(userQueryObject.RoleName))
            {
                var role = Enum.Parse<UserRoles>(userQueryObject.RoleName);
                query = query.Where(u => u.Role == role);
            }
            if (isVerified != null) query = query.Where(u => u.IsVerified == isVerified);
        }

        return await query.ToListAsync();
    }

    public async Task<User> GetAsync(int? id = null, int? employeeid = null, string? email = null)
    {
        IQueryable<User> query = _dbContext.Users;

        if (id != null) query = query.Where(u => u.Id == id);
        if (employeeid != null) query = query.Where(u => u.EmployeeID == employeeid);
        if (email != null) query = query.Where(u => u.Email == email);

        return await query.FirstOrDefaultAsync();
    }

    public async Task StoreAsync(User user)
    {
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(User user)
    {
        user.IsDeleted = true;
        await _dbContext.SaveChangesAsync();
    }
}