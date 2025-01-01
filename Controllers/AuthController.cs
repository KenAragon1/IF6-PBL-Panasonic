using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using panasonic.Dtos.UserDto;
using panasonic.Exceptions;
using panasonic.Services;
using panasonic.ViewModels;

namespace panasonic.Controllers;

public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    public IActionResult Login()
    {
        if (User.Identity!.IsAuthenticated) return RedirectToAction("Index", "Dashboard");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(UserLoginDto userLoginDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new LoginViewModel { UserLoginDto = userLoginDto };
                return View(viewModel);
            }

            await _authService.LoginAsync(userLoginDto);
            return RedirectToAction("Index", "Dashboard");

        }
        catch (OperationNotAllowed e)
        {
            var viewModel = new LoginViewModel { UserLoginDto = userLoginDto };
            ModelState.AddModelError(string.Empty, e.Message);
            return View(viewModel);
        }
        catch (System.Exception)
        {
            var viewModel = new LoginViewModel { UserLoginDto = userLoginDto };
            ModelState.AddModelError(string.Empty, "Something went wrong");
            return View(viewModel);
        }

    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
    {
        if (!ModelState.IsValid)
        {
            var viewModel = new RegisterViewModel { UserRegisterDto = userRegisterDto };
            return View(viewModel);
        }

        await _authService.RegisterAsync(userRegisterDto);

        TempData["SuccessMessage"] = "Account successfully created. Please wait for an admin to verify your account";

        return RedirectToAction("Login");
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }





}