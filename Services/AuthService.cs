using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using panasonic.Dtos.UserDto;
using panasonic.Exceptions;
using panasonic.Models;
using panasonic.Repositories;

namespace panasonic.Services;

public interface IAuthService
{
    Task LoginAsync(UserLoginDto userLoginDto);
    Task RegisterAsync(UserRegisterDto userRegisterDto);
}

public class AuthService : IAuthService
{
    private readonly PasswordHasher<User> _passwordHasher;
    private readonly ApplicationDbContext _dbContext;
    private readonly IUserRepository _userRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(ApplicationDbContext dbContext, IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
    {
        _passwordHasher = new PasswordHasher<User>();
        _dbContext = dbContext;
        _userRepository = userRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task LoginAsync(UserLoginDto userLoginDto)
    {
        var user = await _userRepository.GetAsync(employeeid: userLoginDto.EmployeeID);

        if (user == null || user.EmployeeID != userLoginDto.EmployeeID || !IsPasswordCorrect(user, userLoginDto.Password))
        {
            throw new OperationNotAllowed("Username or password incorrect");
        }

        if (!user.IsVerified)
        {
            throw new OperationNotAllowed("Your account needs to be verified first by an Admin");
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Fullname),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim("UserId", user.Id.ToString()),
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var claimPrincipal = new ClaimsPrincipal(claimsIdentity);

        await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal);
    }

    public async Task RegisterAsync(UserRegisterDto userRegisterDto)
    {
        var user = new User
        {
            EmployeeID = userRegisterDto.EmployeeID,
            Email = userRegisterDto.Email,
            Fullname = userRegisterDto.Fullname,
            Role = UserRoles.Guest,
            IsVerified = false
        };
        var HashedPassword = _passwordHasher.HashPassword(user, userRegisterDto.Password);
        user.HashedPassword = HashedPassword;

        await _userRepository.StoreAsync(user);
    }

    private bool IsPasswordCorrect(User user, string password)
    {
        var result = _passwordHasher.VerifyHashedPassword(user, user.HashedPassword, password);

        return result == PasswordVerificationResult.Success;
    }
}