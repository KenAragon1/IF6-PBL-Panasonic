using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using panasonic.Data.Queries;
using panasonic.Models;
using panasonic.Repositories;
using panasonic.ViewModels.UserViewModel;

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
        var ViewModel = new IndexViewModel
        {
            roles = await _dbContext.Roles.ToListAsync(),
            users = await _userRepository.GetAllAsync(userQueryObject, isVerified: true),
        };

        if (Request.Headers.ContainsKey("x-refresh")) return PartialView("~/Views/Shared/Components/User/_UserTable.cshtml", ViewModel.users);

        return View(ViewModel);
    }

    public async Task<IActionResult> UnverifiedUsers([FromQuery] UserQueryObject userQueryObject)
    {
        var viewModel = new IndexViewModel
        {
            roles = await _dbContext.Roles.ToListAsync(),
            users = await _userRepository.GetAllAsync(userQueryObject, isVerified: false),
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
        var viewModel = new CreateUserViewModel { roles = await _dbContext.Roles.ToListAsync() };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateUserViewModel createUserViewModel)
    {

        if (!ModelState.IsValid)
        {
            var ViewModel = new CreateUserViewModel
            {
                EmployeeID = createUserViewModel.EmployeeID,
                Email = createUserViewModel.Email,
                Fullname = createUserViewModel.Fullname,
                roles = await _dbContext.Roles.ToListAsync()
            };
            return View(ViewModel);
        }

        var user = new User { EmployeeID = createUserViewModel.EmployeeID, Email = createUserViewModel.Email, Fullname = createUserViewModel.Fullname, RoleId = createUserViewModel.RoleId, IsVerified = true };
        user.HashedPassword = _passwordHasher.HashPassword(user, createUserViewModel.Password);
        await _userRepository.StoreAsync(user);


        TempData["SuccessMessage"] = "Create User Success";
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Detail(int Id)
    {
        var user = await _userRepository.GetAsync(id: Id);

        if (user == null) return NotFound();

        var viewModel = new UserDetailViewModel { Id = user.Id, Fullname = user.Fullname, Email = user.Email, EmployeeID = user.EmployeeID, RoleId = user.RoleId, roles = await _dbContext.Roles.ToListAsync() };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int Id, UserDetailViewModel userDetailViewModel)
    {
        var user = await _userRepository.GetAsync(id: Id);

        if (user == null) return NotFound();

        if (!ModelState.IsValid)
        {


            return View(userDetailViewModel);
        }

        user.EmployeeID = userDetailViewModel.EmployeeID;
        user.Email = userDetailViewModel.Email;
        user.Fullname = userDetailViewModel.Fullname;
        user.RoleId = userDetailViewModel.RoleId;

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