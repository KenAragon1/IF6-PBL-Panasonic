using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using panasonic.Models;
using panasonic.Repositories;
using panasonic.Services;
using panasonic.ViewModels.MaterialInventoryViewModel;

namespace panasonic.Controllers;

[Authorize]
public class MaterialInventoryController : BaseController
{
    private readonly IMaterialInventoryService _materialInventoryService;
    private readonly IProductionLineRepository _productionLineRepository;
    private readonly IMaterialInventoryRepository _materialInventoryRepository;

    public MaterialInventoryController(IMaterialInventoryService materialInventoryService, IProductionLineRepository productionLineRepository, IMaterialInventoryRepository materialInventoryRepository)
    {
        _materialInventoryService = materialInventoryService;
        _productionLineRepository = productionLineRepository;
        _materialInventoryRepository = materialInventoryRepository;
    }

    public IActionResult Reports()
    {
        ViewBag.ReportTypeOptions = Enum.GetValues<TransactionTypes>().Cast<TransactionTypes>().Select(mt => new SelectListItem { Value = mt.ToString(), Text = mt.ToString() });

        return View();
    }

    public async Task<IActionResult> PreperationRoom()
    {
        var viewModel = new PreperationRoomViewModel
        {
            MaterialInventories = await _materialInventoryRepository.GetAllAsync(location: MaterialInventoryLocations.PreperationRoom, withMaterial: true)
        };
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "StoreManager")]
    public IActionResult Send()
    {
        return RedirectToAction();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "AsistantLeader")]
    public IActionResult Return()
    {
        return RedirectToAction();
    }

    public async Task<IActionResult> Pickup()
    {
        var viewModel = new PickupViewModel
        {
            ProductionLineOptions = await _productionLineRepository.GetAllAsync(),
            MaterialInventoryOptions = await _materialInventoryRepository.GetAllAsync(location: MaterialInventoryLocations.PreperationRoom, withMaterial: true)
        };
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "AsistantLeader")]
    public async Task<IActionResult> Pickup(PickupViewModel pickupViewModel)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("validation failed");
                return View();
            }
            await _materialInventoryService.PickupMaterial(pickupViewModel.ProductionLineDestination, pickupViewModel.Forms);

            return RedirectToAction("PreperationRoom");
        }
        catch
        {
            throw;
        }



    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "AsistantLeader")]
    public IActionResult Use()
    {
        return RedirectToAction("");
    }



}