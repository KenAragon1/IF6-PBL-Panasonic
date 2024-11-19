using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using panasonic.Repositories;

namespace panasonic.Controllers;

[Authorize]
public class StoreController : BaseController
{
    private readonly IAreaMaterialRepository _areaMaterialRepository;

    public StoreController(IAreaMaterialRepository areaMaterialRepository)
    {
        _areaMaterialRepository = areaMaterialRepository;
    }
    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Material()
    {
        var materials = await _areaMaterialRepository.GetMaterialsAsync(areaId: 1);
        Console.WriteLine(materials);
        return View(materials);
    }

    public IActionResult CreateMaterialStock()
    {
        return View();
    }
}