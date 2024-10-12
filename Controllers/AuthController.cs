using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using panasonic.Models;
using panasonic.Services;
using panasonic.ViewModels;

namespace panasonic.Controllers;

public class AuthController : Controller
{
    private readonly PasswordHasher<User> _passwordHasher;
    private readonly IUserService _userService;
    public AuthController(UserService userService)
    {
        _passwordHasher = new PasswordHasher<User>();
        _userService = userService;
    }
    public IActionResult Login()
    {
        if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Dashboard");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel LoginForm)
    {

        if (!ModelState.IsValid) return View();

        var user = await _userService.GetUserByEmployeeID(LoginForm.EmployeeID);

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


        if (user.Role.RoleName == "unverified")
        {
            ViewBag.Message = "Your account needs to be verified by an Admin first";
            return View();
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Fullname),
            new Claim(ClaimTypes.Role, user.Role.RoleName)
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

        var existingEmployeeId = await _userService.GetUserByEmployeeID(RegisterData.EmployeeID);
        var existingEmail = await _userService.GetUserByEmail(RegisterData.Email);

        if (existingEmployeeId != null)
        {
            ViewBag.ErrorMessage = "EmployeeId is already taken";
            return View();
        }

        if (existingEmail != null)
        {
            ViewBag.ErrorMessage = "Email is already taken";
            return View();
        }

        var user = new Models.User { EmployeeID = RegisterData.EmployeeID, Email = RegisterData.Email, Fullname = RegisterData.Fullname, RoleId = 1 };
        var HashedPassword = _passwordHasher.HashPassword(user, RegisterData.Password);
        user.HashedPassword = HashedPassword;

        await _userService.InsertUser(user);

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