using Microsoft.AspNetCore.Mvc;
using panasonic.Repositories;
using panasonic.ViewModels.AreaViewModel;

namespace panasonic.Controllers;

public class AreaController : BaseController
{
    private readonly IAreaRepository _areaRepository;

    public AreaController(IAreaRepository areaRepository)
    {
        _areaRepository = areaRepository;
    }
    public async Task<IActionResult> Index()
    {
        var viewModel = new IndexViewModel
        {
            areas = await _areaRepository.GetAreasAsync()
        };
        return View(viewModel);
    }

    public async Task<IActionResult> Manage(int Id)
    {
        var viewModel = new ManageViewModel
        {
            area = await _areaRepository.GetAreaAsync(Id, true)
        };
        Console.WriteLine(viewModel.area.Specifier);

        return View(viewModel);
    }
}