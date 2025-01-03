using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using panasonic.ViewModels.DashboardViewModel;
using panasonic.Models;
using System.Transactions;
using System.Text.RegularExpressions;

namespace panasonic.Controllers;

[Authorize]
public class DashboardController : BaseController
{
    private readonly ApplicationDbContext _dbContext;

    public DashboardController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IActionResult> Index()
    {
        var materialCount = await _dbContext.Materials.Where(m => !m.IsDeleted).CountAsync();
        var userCount = await _dbContext.Users.CountAsync();
        var productionCount = await _dbContext.ProductionLines.CountAsync();


        var mtd = await _dbContext.MaterialTransactionDetails
        .Include(mtd => mtd.MaterialTransaction)
        .Include(mtd => mtd.Material)
        .GroupBy(mtd => mtd.MaterialTransaction!.Type)
        .ToListAsync();

        var usedMaterialForProduction = mtd.Where(m => m.Key == TransactionTypes.Production)
        .SelectMany(g => g)
        .GroupBy(m => m.Material!.Name)
        .Select(m => new MaterialUsedForProduction { MaterialName = m.Key, QuantityUsed = m.Sum(m => m.Quantity) })
        .ToList();

        var count = mtd.Select(c => new TransactionCount { Type = c.Key, Count = c.Count() }).ToList();

        var viewModel = new IndexViewModel
        {
            Materials = usedMaterialForProduction,
            TransactionCounts = count,
            MaterialCount = materialCount,
            ProductionLineCount = productionCount,
            UserCount = userCount,
            AnalitycsDatas = await generateAnalyticsData()
        };


        return View(viewModel);
    }

    public async Task<IActionResult> Report()
    {
        var viewModel = new ReportViewModel { MaterialTransactions = await _dbContext.MaterialTransactions.Include(mt => mt.MaterialTransactionDetails).ThenInclude(m => m.Material).OrderByDescending(mt => mt.CreatedAt).AsNoTracking().ToListAsync() };
        return View(viewModel);
    }

    private async Task<List<AnalitycsData>> generateAnalyticsData()
    {
        var sevenDaysAgo = DateTime.Now.Date.AddDays(-7);
        var today = DateTime.Now.Date;

        var dateRange = Enumerable.Range(0, (today - sevenDaysAgo).Days + 1)
        .Select(offset => sevenDaysAgo.AddDays(offset))
        .ToList();

        var types = _dbContext.MaterialTransactions.Select(x => x.Type).Distinct().ToList();

        var data = await _dbContext.MaterialTransactions
        .Where(mt => mt.CreatedAt.Date >= sevenDaysAgo)
        .GroupBy(mt => new { mt.Type, Date = mt.CreatedAt.Date })
        .Select(group => new
        {
            Type = group.Key.Type.ToString(),
            Date = group.Key.Date,
            Count = group.Count()
        }).ToListAsync();

        var result = types
    .SelectMany(type => dateRange.Select(date => new { Type = type.ToString(), Date = date }))
    .GroupJoin(
        data,
        td => new { Type = td.Type.ToString(), Date = td.Date },
        d => new { Type = d.Type, Date = d.Date },
        (td, group) => new AnalitycsData
        {
            Type = td.Type.ToString(),
            Date = td.Date.ToString("yyyy-MM-dd"),
            Count = group.FirstOrDefault()?.Count ?? 0
        })
    .OrderBy(x => x.Type)
    .ThenBy(x => x.Date)
    .ToList();

        return result;

    }



}
