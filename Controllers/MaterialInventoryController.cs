using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using panasonic.Errors;
using panasonic.Models;
using panasonic.Repositories;
using panasonic.Services;
using panasonic.ViewModels.MaterialInventoryViewModel;
using panasonic.ViewModels.MaterialInventoryViewModels;

namespace panasonic.Controllers;

[Authorize]
public class MaterialInventoryController : BaseController
{
    private readonly IMaterialInventoryService _materialInventoryService;
    private readonly IProductionLineRepository _productionLineRepository;

    private readonly IMaterialService _materialService;
    private readonly IMaterialRequestRepository _materialRequestRepository;

    public MaterialInventoryController(IMaterialService materialService, IMaterialInventoryService materialInventoryService, IProductionLineRepository productionLineRepository, IMaterialRequestRepository materialRequestRepository)
    {
        _materialInventoryService = materialInventoryService;
        _productionLineRepository = productionLineRepository;
        _materialService = materialService;
        _materialRequestRepository = materialRequestRepository;
    }

    public async Task<IActionResult> PreperationRoom()
    {

        var inventories = await _materialInventoryService.GetAllAsync(mi => mi.Location == MaterialInventoryLocations.PreperationRoom);
        var viewModel = new PreperationRoomViewModel
        {
            Inventories = inventories.Select(i => new InventoryListItemViewModel
            {
                InventoryId = i.Id,
                MaterialName = i.Material!.Name,
                MaterialBarcode = i.Material!.Barcode,
                MaterialNumber = i.Material.Number,
                InventoryAvailableQuantity = i.Quantity,
                MaterialDetailMeasurement = i.Material!.DetailMeasurement
            }).ToList()

        };
        return View(viewModel);
    }

    public async Task<IActionResult> ProductionLine()
    {
        var inventories = await _materialInventoryService.GetAllAsync(mi => mi.Location == MaterialInventoryLocations.ProductionLine);
        var viewModel = new ProductionLineViewModel
        {
            Inventories = inventories.Select(i => new InventoryListItemViewModel
            {
                InventoryId = i.Id,
                MaterialName = i.Material!.Name,
                MaterialBarcode = i.Material!.Barcode,
                MaterialNumber = i.Material.Number,
                InventoryAvailableQuantity = i.Quantity,
                MaterialDetailMeasurement = i.Material!.DetailMeasurement,
                ProductionLineRemark = i.ProductionLine!.Remark
            }).ToList()

        };
        return View(viewModel);
    }

    public async Task<IActionResult> Send()
    {
        var viewModel = new SendViewModel
        {
            MaterialRequests = await _materialRequestRepository.GetAllByCondition(mr => mr.Status == MaterialRequestStatus.Approved && mr.FullfilledQuantity < mr.RequestedQuantity)
        };
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
                var viewModel = new SendViewModel { };
                return View(viewModel);
            }
            TempData["SuccessMessage"] = "Material send to preperation room success";
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
        var viewModel = await GenerateReturnViewModel();
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "AsistantLeader")]
    public async Task<IActionResult> Return(ReturnInventoryViewModel returnInventoryViewModel)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                var viewModel = await GenerateReturnViewModel(returnInventoryViewModel.InventoryForms, returnInventoryViewModel.ProductionLineId);
                return View(viewModel);
            }



            await _materialInventoryService.ReturnMaterialAsync(returnInventoryViewModel);

            TempData["SuccessMessage"] = "Return material success";

            return RedirectToAction("ProductionLine");
        }
        catch (ExceptionWithModelError e)
        {
            ModelState.AddModelError(e.ModelKey, e.Message);
            var viewModel = await GenerateReturnViewModel(returnInventoryViewModel.InventoryForms, returnInventoryViewModel.ProductionLineId);
            return View(viewModel);
        }
        catch (System.Exception)
        {
            ModelState.AddModelError(string.Empty, "Internal Server Error");
            var viewModel = await GenerateReturnViewModel(returnInventoryViewModel.InventoryForms, returnInventoryViewModel.ProductionLineId);
            return View(viewModel);
        }
    }

    public async Task<IActionResult> Pickup()
    {
        var viewModel = await GeneratePickupViewModel();

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "AsistantLeader")]
    public async Task<IActionResult> Pickup(PickupInventoryViewModel pickupInventoryViewModel)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                var viewModel = await GeneratePickupViewModel(pickupInventoryViewModel.InventoryForms, pickupInventoryViewModel.ProductionLineId);
                return View(viewModel);
            }
            await _materialInventoryService.PickupMaterial(pickupInventoryViewModel);

            TempData["SuccessMessage"] = "Material pickup to production line success";

            return RedirectToAction("PreperationRoom");
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Something went wrong";
            var viewModel = await GeneratePickupViewModel(pickupInventoryViewModel.InventoryForms, pickupInventoryViewModel.ProductionLineId);
            return View(viewModel);
        }
    }


    public async Task<IActionResult> Use()
    {
        var viewModel = await GenerateUseViewModel();
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "AsistantLeader")]
    public async Task<IActionResult> Use(UseInventoryViewModel useInventoryViewModel)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                var viewModel = await GenerateUseViewModel();
                return View(viewModel);
            }

            await _materialInventoryService.UseMaterialAsync(useInventoryViewModel);

            TempData["SuccessMessage"] = "Material used success";


            return RedirectToAction("ProductionLine");
        }
        catch (ExceptionWithModelError e)
        {
            ModelState.AddModelError(e.ModelKey, e.Message);
            var viewModel = await GenerateUseViewModel();
            return View(viewModel);
        }
        catch (System.Exception)
        {
            ModelState.AddModelError(string.Empty, "Internal Server Error");
            var viewModel = await GenerateUseViewModel();
            return View(viewModel);
        }


    }

    private async Task<PickupInventoryViewModel> GeneratePickupViewModel(List<InventoryFormViewModel>? inventoryForms = null, int? productionLineId = null)
    {
        return new PickupInventoryViewModel
        {
            Materials = await _materialService.GetAllAsync(),
            ProductionLines = await _productionLineRepository.GetAllAsync(),
            Inventories = await _materialInventoryService.GetAllAsync(mi => mi.Location == MaterialInventoryLocations.PreperationRoom),
            InventoryForms = inventoryForms ?? new List<InventoryFormViewModel>(),
            ProductionLineId = productionLineId ?? 0
        };
    }
    private async Task<ReturnInventoryViewModel> GenerateReturnViewModel(List<InventoryFormViewModel>? inventoryForms = null, int? productionLineId = null)
    {
        return new ReturnInventoryViewModel
        {
            Materials = await _materialService.GetAllAsync(),
            ProductionLines = await _productionLineRepository.GetAllAsync(),
            Inventories = await _materialInventoryService.GetAllAsync(mi => mi.Location == MaterialInventoryLocations.ProductionLine),
            InventoryForms = inventoryForms ?? new List<InventoryFormViewModel>(),
            ProductionLineId = productionLineId ?? 0
        };
    }

    private async Task<UseInventoryViewModel> GenerateUseViewModel(List<InventoryFormViewModel>? inventoryForms = null, int? productionLineId = null)
    {
        return new UseInventoryViewModel
        {
            Materials = await _materialService.GetAllAsync(),
            ProductionLines = await _productionLineRepository.GetAllAsync(),
            Inventories = await _materialInventoryService.GetAllAsync(mi => mi.Location == MaterialInventoryLocations.ProductionLine),
            InventoryForms = inventoryForms ?? new List<InventoryFormViewModel>(),
            ProductionLineId = productionLineId ?? 0
        };
    }

}