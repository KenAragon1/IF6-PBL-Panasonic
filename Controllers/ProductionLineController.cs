using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using panasonic.Data.Queries;
using panasonic.Models;
using panasonic.Repositories;
using panasonic.ViewModels.ProductionLineViewModel;

namespace panasonic.Controllers;

[Authorize(Roles = "ShiftLeader,AsistantLeader,Admin")]
public class ProductionLineController : BaseController
{
    private readonly IAreaRepository _areaRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMaterialRepository _materialRepository;

    public ProductionLineController(IAreaRepository areaRepository, IUserRepository userRepository, IMaterialRepository materialRepository)
    {
        _areaRepository = areaRepository;
        _userRepository = userRepository;
        _materialRepository = materialRepository;
    }

    public async Task<IActionResult> Index()
    {

        var viewModel = new IndexViewModel
        {
            areas = await _areaRepository.GetAreasAsync("ProductionLine")
        };

        return View(viewModel);
    }

    public async Task<IActionResult> Manage(int Id)
    {
        var viewModel = new ManageViewModel
        {
            users = await _userRepository.GetAllAsync(new UserQueryObject { AreaId = Id }),
            materials = await _materialRepository.GetAllAsync()
        };
        return View(viewModel);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateViewModel createViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(createViewModel);
        }

        var area = new Area { Remark = createViewModel.LineNumber, Type = AreaTypes.ProductionLine };

        await _areaRepository.Create(area);

        TempData["SuccessMessage"] = "Create New Production Line Success";

        return RedirectToAction("Index");
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult AddUser()
    {
        return View();
    }

    [Route("/ProductionLine/{areaId}/MaterialRequest")]
    public IActionResult MaterialRequest(int areaId)
    {
        return View();
    }










}