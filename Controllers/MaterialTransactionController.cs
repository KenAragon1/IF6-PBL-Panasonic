using Microsoft.AspNetCore.Mvc;
using panasonic.Repositories;
using panasonic.ViewModels.ReportViewModel;

namespace panasonic.Controllers;

public class MaterialTransactionController : BaseController
{
    private readonly IMaterialTransactionRepository _materialTransactionRepository;

    public MaterialTransactionController(IMaterialTransactionRepository materialTransactionRepository)
    {
        _materialTransactionRepository = materialTransactionRepository;
    }

    public async Task<IActionResult> Index()
    {

        var viewModel = new IndexViewModel { MaterialTransactions = await _materialTransactionRepository.GetAllAsync() };
        return View(viewModel);
    }

    public async Task<IActionResult> Detail(int Id)
    {
        var materialTransaction = await _materialTransactionRepository.GetByIdAsync(Id);

        if (materialTransaction == null) return NotFound();

        var viewModel = new DetailViewModel { MaterialTransaction = materialTransaction };
        return View(viewModel);
    }


}