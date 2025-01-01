using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using panasonic.Dtos.ProductionLineDtos;
using panasonic.Models;
using panasonic.Repositories;
using panasonic.ViewModels.ProductionLineViewModels;

namespace panasonic.Controllers;

[Authorize(Roles = "ShiftLeader,AsistantLeader,Admin")]
public class ProductionLineController : BaseController
{
    private readonly IProductionLineRepository _productionLineRepository;

    public ProductionLineController(IProductionLineRepository areaRepository)
    {
        _productionLineRepository = areaRepository;
    }

    public async Task<IActionResult> Index()
    {
        var ProductionLines = await _productionLineRepository.GetAllAsync();

        return View(ProductionLines);
    }



    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductionLineDto productionLineDto)
    {
        if (!ModelState.IsValid)
        {
            var viewModel = new ProductionLineViewModel { ProductionLineDto = productionLineDto };
            return View(viewModel);
        }

        var area = new ProductionLine { Remark = productionLineDto.Remark, Description = productionLineDto.Description };

        await _productionLineRepository.Create(area);

        TempData["SuccessMessage"] = "Create New Production Line Success";

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(int Id)
    {
        var productionLine = await _productionLineRepository.GetAsync(Id);
        var lineDto = new ProductionLineDto
        {
            Id = productionLine.Id,
            Description = productionLine.Description,
            Remark = productionLine.Remark
        };
        var viewModel = new ProductionLineViewModel { ProductionLineDto = lineDto };
        return View(viewModel);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int Id, ProductionLineDto productionLineDto)
    {
        if (!ModelState.IsValid)
        {
            productionLineDto.Id = Id;
            var viewModel = new ProductionLineViewModel { ProductionLineDto = productionLineDto };
            return View(viewModel);
        }

        var line = await _productionLineRepository.GetAsync(Id);
        line.Remark = productionLineDto.Remark;
        line.Description = productionLineDto.Description;
        await _productionLineRepository.UpdateAsync(line);

        TempData["SuccessMessage"] = "Edit Production Line Success";

        return RedirectToAction("Index");
    }

}