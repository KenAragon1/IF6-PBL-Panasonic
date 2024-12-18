using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using panasonic.Errors;
using panasonic.Exceptions;
using panasonic.Services;
using panasonic.ViewModels.MaterialViewModel;

namespace panasonic.Controllers;

[Authorize(Roles = "StoreManager")]
public class MaterialController : BaseController
{
    private readonly IMaterialService _materialService;

    public MaterialController(IMaterialService materialService)
    {
        _materialService = materialService;
    }

    public async Task<IActionResult> Index()
    {
        var material = await _materialService.GetAllAsync();
        return View(material);
    }
    public IActionResult Create()
    {
        var viewModel = new CreateMaterialViewModel();
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateMaterialViewModel createMaterialViewModel)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return View(createMaterialViewModel);
            }
            await _materialService.CreateAsync(createMaterialViewModel);

            TempData["SuccessMessage"] = "New Mateial Added";
            return RedirectToAction("Index");
        }
        catch (ExceptionWithModelError e)
        {
            ModelState.AddModelError(e.ModelKey, e.Message);
            return View(createMaterialViewModel);
        }
        catch (System.Exception)
        {
            TempData["SuccessMessage"] = "New Mateial Added";
            return View(createMaterialViewModel);
        }



    }

    public async Task<IActionResult> Delete(int Id)
    {
        try
        {
            await _materialService.DeleteAsync(Id);

            TempData["SuccessMessage"] = "Delete Material Success";

            return RedirectToAction("Index");
        }
        catch (ItemNotFoundException e)
        {
            TempData["ErrorMessage"] = e.Message;
            return RedirectToAction("Index");
        }
        catch (System.Exception)
        {
            TempData["ErrorMessage"] = "Something Went Wrong";
            return RedirectToAction("Index");

        }

    }



    public async Task<IActionResult> Edit(int Id)
    {
        try
        {
            var material = await _materialService.GetByIdAsync(Id);

            var viewModel = _materialService.MaterialViewModel(material);

            return View(viewModel);
        }
        catch (ItemNotFoundException e)
        {
            TempData["ErrorMessage"] = e.Message;
            return RedirectToAction("Index");
        }
        catch (System.Exception)
        {
            TempData["ErrorMessage"] = "Something went wrong";
            return RedirectToAction("Index");
        }


    }

    [HttpPost]
    public async Task<IActionResult> Edit(int Id, EditMaterialViewModel editMaterialViewModel)
    {

        try
        {
            if (!ModelState.IsValid)
            {
                return View(editMaterialViewModel);
            }

            await _materialService.UpdateAsync(editMaterialViewModel);

            TempData["SuccessMessage"] = "Update Material Success";

            return RedirectToAction("Index");

        }
        catch (ItemNotFoundException e)
        {
            TempData["ErrorMessage"] = e.Message;
            return View(editMaterialViewModel);
        }
        catch (System.Exception)
        {
            TempData["ErrorMessage"] = "Something went wrong";
            return View(editMaterialViewModel);
        }

    }


}