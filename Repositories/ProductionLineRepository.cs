using Microsoft.EntityFrameworkCore;
using panasonic.Models;

namespace panasonic.Repositories;

public interface IProductionLineRepository
{
    Task<List<ProductionLine>> GetAllAsync();
    Task<ProductionLine> GetAsync(int lineId);
    Task Create(ProductionLine area);
    Task UpdateAsync(ProductionLine line);
    Task DeleteAsync(int id);
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

    public async Task UpdateAsync(ProductionLine line)
    {
        _dbContext.Update(line);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var line = await _dbContext.ProductionLines.FirstOrDefaultAsync(pl => pl.Id == id);

        if (line == null) throw new Exception();

        line.IsDeleted = true;

        await _dbContext.SaveChangesAsync();
    }

}