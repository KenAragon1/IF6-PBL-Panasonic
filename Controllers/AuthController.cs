using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using panasonic.Models;
using panasonic.Repositories;
using panasonic.ViewModels;

namespace panasonic.Controllers;

public class AuthController : Controller
{
    private readonly PasswordHasher<User> _passwordHasher;
    private readonly IUserRepository _userRepository;

    public AuthController(IUserRepository userRepository)
    {
        _passwordHasher = new PasswordHasher<User>();
        _userRepository = userRepository;
    }
    public IActionResult Login()
    {
        if (User.Identity!.IsAuthenticated) return RedirectToAction("Index", "Dashboard");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel LoginForm)
    {
        if (!ModelState.IsValid) return View();

        var user = await _userRepository.GetAsync(employeeid: LoginForm.EmployeeID);

        if (user == null)
        {
            ViewBag.Message = "Employee ID or Password Incorrect";
            return View();
        }

        if (user.EmployeeID != LoginForm.EmployeeID || !IsPasswordCorrect(user, LoginForm.Password))
        {
            ViewBag.Message = "Employee ID or Password Incorrect 123";
            return View();
        }

        if (!user.IsVerified)
        {
            ViewBag.Message = "Your account needs to be verified by an Admin first";
            return View();
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Fullname),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim("UserId", user.Id.ToString()),
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var claimPrincipal = new ClaimsPrincipal(claimsIdentity);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal);

        return RedirectToAction("Index", "Dashboard");
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel RegisterData)
    {
        if (!ModelState.IsValid) return View();



        var user = new User { EmployeeID = RegisterData.EmployeeID, Email = RegisterData.Email, Fullname = RegisterData.Fullname, Role = UserRoles.Guest, IsVerified = false };
        var HashedPassword = _passwordHasher.HashPassword(user, RegisterData.Password);
        user.HashedPassword = HashedPassword;

        await _userRepository.StoreAsync(user);

        TempData["SuccessMessage"] = "Account successfully created. Please wait for an admin to verify your account";

        return RedirectToAction("Login");
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }

    public bool IsPasswordCorrect(User user, string password)
    {
        var result = _passwordHasher.VerifyHashedPassword(user, user.HashedPassword, password);

        return result == PasswordVerificationResult.Success;
    }
}