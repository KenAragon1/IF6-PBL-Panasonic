using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using panasonic.Models;
using panasonic.Repositories;
using panasonic.ViewModels.MaterialRequestViewModel;

namespace panasonic.Controllers;

[Authorize]
public class MaterialRequestController : BaseController
{
    private readonly IMaterialRequestRepository _materialRequestRepository;
    private readonly IProductionLineRepository _productionLineRepository;
    private readonly IMaterialRepository _materialRepository;
    public MaterialRequestController(IMaterialRequestRepository materialRequestRepository, IProductionLineRepository areaRepository, IMaterialRepository materialRepository)
    {
        _materialRequestRepository = materialRequestRepository;
        _productionLineRepository = areaRepository;
        _materialRepository = materialRepository;
    }
    public async Task<IActionResult> Index()
    {
        var query = _materialRequestRepository.Query();

        switch (User.FindFirst(ClaimTypes.Role)?.Value)
        {
            case "AsistantLeader":
                query = query.Where(mr => mr.RequestedById == int.Parse(User.FindFirst("UserId")!.Value));
                break;
            case "StoreManager":
                query = query.Where(mr => mr.Status != MaterialRequestStatus.Pending);
                break;
        }

        return View(await query.Include(mr => mr.RequestedBy).Include(mr => mr.Material).Include(mr => mr.ProductionLine).ToListAsync());
    }

    [Authorize(Roles = "AsistantLeader")]
    public async Task<IActionResult> Create()
    {
        var ViewModels = new CreateViewModel { Materials = await _materialRepository.GetAllAsync(), ProductionLines = await _productionLineRepository.GetAllAsync() };
        return View(ViewModels);
    }

    [HttpPost]
    [Authorize(Roles = "AsistantLeader")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateViewModel createViewModel)
    {
        if (!ModelState.IsValid)
        {
            var ViewModels = new CreateViewModel { Materials = await _materialRepository.GetAllAsync(), ProductionLines = await _productionLineRepository.GetAllAsync() };
            return View(ViewModels);
        }

        int.TryParse(User.FindFirst("UserId")!.Value, out int userId);
        var materialRequest = createViewModel.CreateForms.Select(f => new MaterialRequest { MaterialId = f.MaterialId, ProductionLineId = f.ProductionLineId, Quantity = f.Quantity, RequestedById = userId }).ToList();
        await _materialRequestRepository.StoreManyAsync(materialRequest);

        TempData["SuccessMessage"] = "Material Request Created";
        return RedirectToAction("Index");
    }

    [HttpPost]
    [Authorize(Roles = "ShiftLeader")]
    [ValidateAntiForgeryToken]
    [Route("MaterialRequest/{Id}/Verify")]
    public async Task<IActionResult> Verify(int Id)
    {
        var request = await _materialRequestRepository.GetAsync(Id);
        request.SetToVerified(int.Parse(User.FindFirst("UserId")!.Value));
        await _materialRequestRepository.UpdateAsync(request);

        return RedirectToAction("Index");
    }
    [HttpPost]
    [Authorize(Roles = "StoreManager")]
    [ValidateAntiForgeryToken]
    [Route("MaterialRequest/{Id}/Accept")]
    public async Task<IActionResult> Accept(int Id)
    {
        var request = await _materialRequestRepository.GetAsync(Id);
        request.Accept(int.Parse(User.FindFirst("UserId")!.Value));
        await _materialRequestRepository.UpdateAsync(request);

        return RedirectToAction("Index");
    }

    [HttpPost]
    [Authorize(Roles = "ShiftLeader,StoreManager")]
    [ValidateAntiForgeryToken]
    [Route("MaterialRequest/{Id}/Reject")]
    public async Task<IActionResult> Reject(int Id)
    {
        var request = await _materialRequestRepository.GetAsync(Id);
        request.Reject(int.Parse(User.FindFirst("UserId")!.Value));
        await _materialRequestRepository.UpdateAsync(request);

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _materialRequestRepository.DeleteAsync(id);
        return RedirectToAction("Index");
    }

}

