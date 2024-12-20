using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using panasonic.Models;
using panasonic.Repositories;
using panasonic.Services;
using panasonic.ViewModels.MaterialRequestViewModel;

namespace panasonic.Controllers;

[Authorize]
public class MaterialRequestController : BaseController
{
    private readonly IMaterialRequestRepository _materialRequestRepository;
    private readonly IProductionLineRepository _productionLineRepository;
    private readonly IMaterialRepository _materialRepository;
    private readonly IMaterialRequestService _materialRequestService;

    public MaterialRequestController(IMaterialRequestRepository materialRequestRepository, IProductionLineRepository areaRepository, IMaterialRepository materialRepository, IMaterialRequestService materialRequestService)
    {
        _materialRequestRepository = materialRequestRepository;
        _productionLineRepository = areaRepository;
        _materialRepository = materialRepository;
        _materialRequestService = materialRequestService;
    }
    public async Task<IActionResult> Index()
    {
        List<MaterialRequest> materialRequests = new List<MaterialRequest>();
        switch (User.FindFirst(ClaimTypes.Role)?.Value)
        {
            case "AsistantLeader":
                materialRequests = await _materialRequestRepository.GetAllByCondition(mr => mr.RequestedById == int.Parse(User.FindFirst("UserId")!.Value));
                break;
            case "StoreManager":
                materialRequests = await _materialRequestRepository.GetAllByCondition(mr => mr.Status == MaterialRequestStatus.Verified || mr.Status == MaterialRequestStatus.Approved || (mr.Status == MaterialRequestStatus.Rejected && mr.RejectedBy!.Role == UserRoles.StoreManager));
                break;
            case "ShiftLeader":
                materialRequests = await _materialRequestRepository.GetAllAsync(); ;
                break;
        }

        return View(materialRequests);
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

        try
        {
            if (!ModelState.IsValid)
            {
                var ViewModels = new CreateViewModel { Materials = await _materialRepository.GetAllAsync(), ProductionLines = await _productionLineRepository.GetAllAsync() };
                return View(ViewModels);
            }

            await _materialRequestService.CreateAsync(createViewModel);

            TempData["SuccessMessage"] = "Material Request Created";
            return RedirectToAction("Index");
        }
        catch (System.Exception)
        {
            throw;
        }

    }

    [HttpPost]
    [Authorize(Roles = "ShiftLeader")]
    [ValidateAntiForgeryToken]
    [Route("MaterialRequest/{Id}/Verify")]
    public async Task<IActionResult> Verify(int Id)
    {
        try
        {
            await _materialRequestService.VerifyAsync(Id);
            TempData["SuccessMessage"] = "Request Verified";
            return RedirectToAction("Index");
        }
        catch (System.Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            throw;
            return RedirectToAction("Index");
        }

    }

    [HttpPost]
    [Authorize(Roles = "StoreManager")]
    [ValidateAntiForgeryToken]
    [Route("MaterialRequest/{Id}/Accept")]
    public async Task<IActionResult> Approve(int Id)
    {
        try
        {
            await _materialRequestService.ApproveAsync(Id);
            TempData["SuccessMessage"] = "Request Approved";
            return RedirectToAction("Index");
        }
        catch (System.Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    [Authorize(Roles = "ShiftLeader,StoreManager")]
    [ValidateAntiForgeryToken]
    [Route("MaterialRequest/{Id}/Reject")]
    public async Task<IActionResult> Reject(int Id)
    {
        try
        {
            await _materialRequestService.RejectAsync(Id);
            TempData["SuccessMessage"] = "Request Rejected";
            return RedirectToAction("Index");
        }
        catch (System.Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return RedirectToAction("Index");
        }

    }

    [HttpPost]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            await _materialRequestRepository.DeleteAsync(id);
            TempData["SuccessMessage"] = "Request Cancelled";
            return RedirectToAction("Index");
        }
        catch (System.Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return RedirectToAction("Index");
        }

    }

}

