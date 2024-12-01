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
    private readonly IProductionLineRepository _productionLineRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMaterialRepository _materialRepository;

    public ProductionLineController(IProductionLineRepository areaRepository, IUserRepository userRepository, IMaterialRepository materialRepository)
    {
        _productionLineRepository = areaRepository;
        _userRepository = userRepository;
        _materialRepository = materialRepository;
    }

    public async Task<IActionResult> Index()
    {

        var viewModel = new IndexViewModel
        {
            ProductionLines = await _productionLineRepository.GetAllAsync()
        };

        return View(viewModel);
    }

    public async Task<IActionResult> Manage(int Id)
    {
        var viewModel = new ManageViewModel
        {
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

        var area = new ProductionLine { Remark = createViewModel.Remark, Description = createViewModel.Description };

        await _productionLineRepository.Create(area);

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