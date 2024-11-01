using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using panasonic.Data.Queries;
using panasonic.Dtos.User;
using panasonic.Models;
using panasonic.Repositories;
using panasonic.ViewModels;

namespace panasonic.Controllers;

[Authorize(Roles = "Admin")]
public class UserController : BaseController
{
    private readonly IUserRepository _userRepository;
    private readonly ApplicationDbContext _dbContext;
    private readonly PasswordHasher<User> _passwordHasher;

    public UserController(IUserRepository userRepository, ApplicationDbContext dbContext)
    {
        _userRepository = userRepository;
        _dbContext = dbContext;
        _passwordHasher = new PasswordHasher<User>();
    }

    public async Task<IActionResult> Index([FromQuery] UserQueryObject userQueryObject)
    {
        var ViewModel = new UserViewModel
        {
            users = await _userRepository.GetAllAsync(userQueryObject, isVerified: true),
            roles = await _dbContext.Roles.ToListAsync()
        };

        return View(ViewModel);
    }

    public async Task<IActionResult> UnverifiedUsers([FromQuery] UserQueryObject userQueryObject)
    {
        var viewModel = new UnverifiedUserViewModel
        {
            users = await _userRepository.GetAllAsync(userQueryObject, isVerified: false),
            roles = await _dbContext.Roles.ToListAsync()
        };
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Verify(int Id)
    {
        var user = await _userRepository.GetAsync(id: Id);
        if (user == null) return NotFound();

        user.IsVerified = true;

        await _userRepository.UpdateAsync(user);

        TempData["SuccessMessage"] = "Verify User Success";
        return RedirectToAction("UnverifiedUsers");
    }


    public async Task<IActionResult> Create()
    {
        var ViewModel = new CreateUserViewModel
        {
            Roles = await _dbContext.Roles.ToListAsync()
        };
        return View(ViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateUserDto createUserDto)
    {

        if (!ModelState.IsValid)
        {
            var ViewModel = new CreateUserViewModel
            {
                Roles = await _dbContext.Roles.ToListAsync()
            };

            return View(ViewModel);
        }

        var user = new User { EmployeeID = createUserDto.EmployeeID, Email = createUserDto.Email, Fullname = createUserDto.Fullname, RoleId = createUserDto.RoleId, IsVerified = true };
        user.HashedPassword = _passwordHasher.HashPassword(user, createUserDto.Password);
        await _userRepository.StoreAsync(user);


        TempData["SuccessMessage"] = "Create User Success";
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(int Id)
    {
        var user = await _userRepository.GetAsync(id: Id);

        if (user == null) return NotFound();

        var viewModel = new EditUserViewModel
        {
            EditUserDto = new EditUserDto { Id = user.Id, Email = user.Email, EmployeeID = user.EmployeeID, Fullname = user.Fullname, RoleId = user.RoleId },
            Roles = await _dbContext.Roles.ToListAsync()
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int Id, EditUserDto editUserDto)
    {
        var user = await _userRepository.GetAsync(id: Id);

        if (user == null) return NotFound();

        if (!ModelState.IsValid)
        {
            var viewModel = new EditUserViewModel
            {
                EditUserDto = new EditUserDto { Id = user.Id, Email = user.Email, EmployeeID = user.EmployeeID, Fullname = user.Fullname, RoleId = user.RoleId },
                Roles = await _dbContext.Roles.ToListAsync()
            };

            return View(viewModel);
        }

        user.EmployeeID = editUserDto.EmployeeID;
        user.Email = editUserDto.Email;
        user.Fullname = editUserDto.Fullname;
        user.RoleId = editUserDto.RoleId;

        await _userRepository.UpdateAsync(user);


        TempData["SuccessMessage"] = "Edit User Success";
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int Id)
    {
        var user = await _userRepository.GetAsync(id: Id);

        if (user == null) return NotFound();

        await _userRepository.DeleteAsync(user);

        TempData["SuccessMessage"] = "Delete User Success";
        return RedirectToAction("Index");
    }
}