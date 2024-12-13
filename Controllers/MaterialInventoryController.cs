using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using panasonic.Errors;
using panasonic.Exceptions;
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
    public async Task<IActionResult> ProductionLine()
    {
        var viewModel = new PreperationRoomViewModel
        {
            MaterialInventories = await _materialInventoryRepository.GetAllAsync(location: MaterialInventoryLocations.ProductionLine, withMaterial: true, withProductionLine: true)
        };
        return View(viewModel);
    }

    public async Task<IActionResult> Send()
    {
        var viewModel = await _materialInventoryService.CreateSendViewModelAsync();
        return View(viewModel);
    }




    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "StoreManager")]
    public async Task<IActionResult> Send(SendViewModel sendViewModel)
    {
        try
        {
            Console.WriteLine(sendViewModel.Forms.Count);
            if (!ModelState.IsValid)
            {
                var viewModel = await _materialInventoryService.CreateSendViewModelAsync(sendViewModel.Forms);
                return View(viewModel);
            }
            await _materialInventoryService.SendMaterialAsync(sendViewModel);
            return RedirectToAction("Index", "MaterialRequest");
        }
        catch (System.Exception)
        {

            throw;
        }
    }

    public async Task<IActionResult> Return()
    {
        var viewModel = await _materialInventoryService.ReturnViewModelAsync();
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "AsistantLeader")]
    public async Task<IActionResult> Return(ReturnViewModel returnViewModel)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                var viewModel = await _materialInventoryService.ReturnViewModelAsync(returnViewModel);
                return View(viewModel);
            }

            await _materialInventoryService.ReturnMaterialAsync(returnViewModel.Forms);

            return RedirectToAction("ProductionLine");
        }
        catch (ExceptionWithModelError e)
        {
            ModelState.AddModelError(e.ModelKey, e.Message);
            var viewModel = await _materialInventoryService.ReturnViewModelAsync(returnViewModel);
            return View(viewModel);
        }
        catch (System.Exception)
        {
            ModelState.AddModelError(string.Empty, "Internal Server Error");
            var viewModel = await _materialInventoryService.ReturnViewModelAsync(returnViewModel);
            return View(viewModel);
        }
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

            TempData["SuccessMessage"] = "Material send to preperation room success";

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