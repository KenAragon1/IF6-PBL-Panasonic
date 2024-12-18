using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            Users = await _userRepository.GetAllAsync(userQueryObject, isVerified: true),
        };

        if (Request.Headers.ContainsKey("x-refresh")) return PartialView("~/Views/Shared/Components/User/_UserTable.cshtml", ViewModel.Users);

        return View(ViewModel);
    }

    public async Task<IActionResult> Unverified([FromQuery] UserQueryObject userQueryObject)
    {
        var viewModel = new IndexViewModel
        {
            Users = await _userRepository.GetAllAsync(userQueryObject, isVerified: false),
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
        return RedirectToAction("Unverified");
    }


    public IActionResult Create()
    {
        var viewModel = new CreateUserViewModel();

        ViewBag.UserRoleOptions = Enum.GetValues(typeof(UserRoles)).Cast<UserRoles>().Select(ur => new SelectListItem { Value = ur.ToString(), Text = ur.ToString() }).ToList();

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateUserViewModel createUserViewModel)
    {

        if (!ModelState.IsValid)
        {
            ViewBag.UserRoleOptions = Enum.GetValues(typeof(UserRoles)).Cast<UserRoles>().Select(ur => new SelectListItem { Value = ur.ToString(), Text = ur.ToString() }).ToList();

            var ViewModel = new CreateUserViewModel
            {
                EmployeeID = createUserViewModel.EmployeeID,
                Email = createUserViewModel.Email,
                Fullname = createUserViewModel.Fullname,
            };
            return View(ViewModel);
        }

        var user = new User { EmployeeID = createUserViewModel.EmployeeID, Email = createUserViewModel.Email, Fullname = createUserViewModel.Fullname, Role = Enum.Parse<UserRoles>(createUserViewModel.Role), IsVerified = true };
        user.HashedPassword = _passwordHasher.HashPassword(user, createUserViewModel.Password);
        await _userRepository.StoreAsync(user);


        TempData["SuccessMessage"] = "Create User Success";
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Detail(int Id)
    {
        var user = await _userRepository.GetAsync(id: Id);

        if (user == null) return NotFound();

        ViewBag.UserRoleOptions = Enum.GetValues(typeof(UserRoles)).Cast<UserRoles>().Select(ur => new SelectListItem { Value = ur.ToString(), Text = ur.ToString() }).ToList();


        var viewModel = new UserDetailViewModel { Id = user.Id, Fullname = user.Fullname, Email = user.Email, EmployeeID = user.EmployeeID, Role = user.Role.ToString() };

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
        user.Role = Enum.Parse<UserRoles>(userDetailViewModel.Role);

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