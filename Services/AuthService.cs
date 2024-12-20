using Azure.Core;
using Microsoft.AspNetCore.Mvc.Routing;
using panasonic.Exceptions;

namespace panasonic.Services;

public interface IAuthService
{

}

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _dbContext;

    public AuthService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }


}