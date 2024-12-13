using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using panasonic.ViewModels.DashboardViewModel;

namespace panasonic.Controllers;

[Authorize]
public class DashboardController : BaseController
{
    private readonly ApplicationDbContext _dbContext;

    public DashboardController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Report()
    {
        var viewModel = new ReportViewModel { MaterialTransactions = await _dbContext.MaterialTransactions.Include(mt => mt.Material).OrderByDescending(mt => mt.CreatedAt).AsNoTracking().ToListAsync() };
        return View(viewModel);
    }


}