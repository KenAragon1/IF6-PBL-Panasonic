using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using panasonic.Models;
using panasonic.Repositories;
using panasonic.ViewModels.StoreViewModel;

namespace panasonic.Controllers;

[Authorize(Roles = "StoreManager")]
public class StoreController : BaseController
{
    private readonly IMaterialInventoryRepository _materialInventoryRepository;
    private readonly IMaterialRepository _materialRepository;
    private readonly IMaterialTransferRepo _materialTransferRepo;
    private readonly IProductionLineRepository _productionLineRepository;

    public StoreController(IMaterialInventoryRepository materialInventoryRepository, IMaterialRepository materialRepository, IProductionLineRepository productionLineRepository, IMaterialTransferRepo materialTransferRepo)
    {
        _materialInventoryRepository = materialInventoryRepository;
        _productionLineRepository = productionLineRepository;
        _materialRepository = materialRepository;
        _materialTransferRepo = materialTransferRepo;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Material()
    {
        var viewModel = new MaterialViewModel { MaterialInventories = await _materialInventoryRepository.GetAllAsync(location: MaterialInventoryLocations.Store) };

        return View(viewModel);
    }

    [Route("/store/material/add")]
    public async Task<IActionResult> Add()
    {
        var viewModel = new AddViewModel { Materials = await _materialRepository.GetAllAsync() };
        return View(viewModel);
    }

    [HttpPost]
    [Route("/store/material/add")]
    public async Task<IActionResult> Add(AddViewModel addViewModel)
    {
        if (!ModelState.IsValid)
        {
            var viewModel = new AddViewModel { Materials = await _materialRepository.GetAllAsync() };
            return View(viewModel);
        }

        var existMaterial = await _materialInventoryRepository.GetAsync(location: MaterialInventoryLocations.Store, materialId: addViewModel.MaterialId);

        if (existMaterial != null)
        {
            existMaterial.Quantity += addViewModel.Quantity;
            await _materialInventoryRepository.UpdateAsync(existMaterial);
        }
        else
        {
            var materialInventory = new MaterialInventory { MaterialId = addViewModel.MaterialId, Quantity = addViewModel.Quantity, Location = MaterialInventoryLocations.Store };
            await _materialInventoryRepository.StoreAsync(materialInventory);
        }

        return RedirectToAction("Material");
    }

    [Route("/store/material/send")]
    public async Task<IActionResult> Send()
    {
        var viewModel = new SendViewModel { MaterialInventories = await _materialInventoryRepository.GetAllAsync() };
        return View(viewModel);
    }


    [HttpPost]
    [Route("/store/material/send")]
    public async Task<IActionResult> Send(SendViewModel sendViewModel)
    {
        await _materialTransferRepo.SendMaterialToPreperationRoom(sendViewModel.SendMaterialForms);

        TempData["SuccessMessage"] = "Material Send To Preperation Room";
        return RedirectToAction("Index");
    }
}