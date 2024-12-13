using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using panasonic.Helpers;
using panasonic.Models;
using panasonic.Repositories;
using panasonic.ViewModels.MaterialViewModel;

namespace panasonic.Controllers;

[Authorize(Roles = "StoreManager")]
public class MaterialController : BaseController
{
    private readonly IMaterialRepository _materialRepository;
    private readonly IFileHelper _fileHelper;

    public MaterialController(IMaterialRepository materialRepository, IFileHelper fileHelper)
    {
        _materialRepository = materialRepository;
        _fileHelper = fileHelper;
    }

    public async Task<IActionResult> Index()
    {
        var material = await _materialRepository.GetAllAsync();
        return View(material);
    }
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateMaterialViewModel createMaterialViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(createMaterialViewModel);
        }

        var material = new Material
        {
            Name = createMaterialViewModel.Name,
            UnitMeasurement = createMaterialViewModel.UnitMeasurement,
            Number = createMaterialViewModel.Number,
            DetailMeasurement = createMaterialViewModel.DetailMeasurement,
            DetailQuantity = createMaterialViewModel.DetailQuantity
        };
        await _materialRepository.StoreAsync(material);

        TempData["SuccessMessage"] = "New Mateial Added";
        return RedirectToAction("Index");


    }

    public async Task<IActionResult> Delete(int Id)
    {

        var material = await _materialRepository.GetAsync(Id);

        if (material == null) return NotFound();


        await _materialRepository.DeleteAsync(material);

        TempData["SuccessMessage"] = "Delete Material Success";

        return RedirectToAction("Index");
    }



    public async Task<IActionResult> Edit(int Id)
    {



        var material = await _materialRepository.GetAsync(Id);

        if (material == null) return NotFound();

        var viewModel = new EditMaterialViewModel
        {
            Id = material.Id,
            Name = material.Name,
            UnitMeasurement = material.UnitMeasurement,
            Number = material.Number,
            DetailMeasurement = material.DetailMeasurement,
            DetailQuantity = material.DetailQuantity
        };
        return View(viewModel);


    }

    [HttpPost]
    public async Task<IActionResult> Edit(int Id, EditMaterialViewModel editMaterialViewModel)
    {

        try
        {
            var material = await _materialRepository.GetAsync(Id);

            if (material == null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(editMaterialViewModel);
            }

            material.Name = editMaterialViewModel.Name;
            material.Number = editMaterialViewModel.Number;
            material.UnitMeasurement = editMaterialViewModel.UnitMeasurement;
            material.DetailMeasurement = material.DetailMeasurement;
            material.DetailQuantity = editMaterialViewModel.DetailQuantity;

            await _materialRepository.UpdateAsync(material);

            TempData["SuccessMessage"] = "Update Material Success";

            return RedirectToAction("Index");

        }
        catch (System.Exception e)
        {
            ModelState.AddModelError("ErrorMessage", e.Message);
            return View(editMaterialViewModel);

        }

    }


}