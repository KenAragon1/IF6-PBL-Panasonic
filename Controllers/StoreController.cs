using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using panasonic.Mappers;
using panasonic.Models;
using panasonic.Repositories;
using panasonic.ViewModels.StoreViewModel;

namespace panasonic.Controllers;

[Authorize]
public class StoreController : BaseController
{
    private readonly IAreaMaterialRepository _areaMaterialRepository;
    private readonly IMaterialRepository _materialRepository;

    public StoreController(IAreaMaterialRepository areaMaterialRepository, IMaterialRepository materialRepository)
    {
        _areaMaterialRepository = areaMaterialRepository;
        _materialRepository = materialRepository;
    }
    public async Task<IActionResult> Index()
    {
        var viewModel = new IndexViewModel { materials = new AreaMaterialMapper().MapToDto(await _areaMaterialRepository.GetAllAsync(areaType: "Store")) };

        return View(viewModel);
    }

    public async Task<IActionResult> CreateMaterialStock()
    {
        var viewModel = new CreateMaterialStockViewModel { Materials = await _materialRepository.GetAllAsync() };
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> CreateMaterialStockAsync(CreateMaterialStockViewModel createMaterialStockViewModel)
    {
        if (!ModelState.IsValid)
        {
            var viewModel = new CreateMaterialStockViewModel { Materials = await _materialRepository.GetAllAsync() };
            return View(viewModel);
        }

        await _areaMaterialRepository.StoreAsync(new AreaMaterial { MaterialId = createMaterialStockViewModel.MaterialId, ExpirationDate = createMaterialStockViewModel.ExpirationDate, Quantity = createMaterialStockViewModel.Quantity, AreaId = 21 });

        TempData["SuccessMessage"] = "Stock Added";
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteMaterialStock(int Id)
    {
        await _areaMaterialRepository.DeleteAsync(Id);

        TempData["SuccessMessage"] = "Stock Deleted";

        return RedirectToAction("Index");
    }
}