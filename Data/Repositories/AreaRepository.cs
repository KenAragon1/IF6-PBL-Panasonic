using Microsoft.EntityFrameworkCore;
using panasonic.Models;

namespace panasonic.Repositories;

public interface IProductionLineRepository
{
    Task<List<ProductionLine>> GetAllAsync();
    Task<ProductionLine> GetAsync(int lineId);
    Task Create(ProductionLine area);
}

public class ProductionLineRepository : IProductionLineRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ProductionLineRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<List<ProductionLine>> GetAllAsync()
    {
        var query = _dbContext.ProductionLines.AsQueryable();

        return await query.ToListAsync();
    }

    public async Task<ProductionLine> GetAsync(int lineId)
    {
        var query = _dbContext.ProductionLines.AsQueryable();


        return await query.FirstAsync(pl => pl.Id == lineId);
    }

    public async Task Create(ProductionLine area)
    {
        await _dbContext.ProductionLines.AddAsync(area);
        await _dbContext.SaveChangesAsync();
    }

}