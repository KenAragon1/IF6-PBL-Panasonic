using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using panasonic.Models;

namespace panasonic.Repositories;

public interface IMaterialInventoryRepository
{
    Task<List<MaterialInventory>> GetAllAsync(MaterialInventoryLocations? location = null, bool withMaterial = false, bool withProductionLine = false);
    Task<List<MaterialInventory>> GetAllByConditionAsync(Expression<Func<MaterialInventory, bool>> predicate);
    Task<MaterialInventory?> GetAsync(int? id = null, int? materialId = null, MaterialInventoryLocations? location = null, int? productionLineId = null);
    Task<MaterialInventory?> GetByConditionAsync(Expression<Func<MaterialInventory, bool>> predicate);
    Task StoreAsync(MaterialInventory materialInventory);
    Task UpdateAsync(MaterialInventory materialInventory);
    Task UpdateManyAsync(List<MaterialInventory> materialInventories);
    Task DeleteAsync(int id);
    Task SaveChangesAsync(List<MaterialTransaction> materialTransactions, List<MaterialInventory> materialInventories);
}


public class MaterialInventoryRepository : IMaterialInventoryRepository
{
    private readonly ApplicationDbContext _dbContext;

    public MaterialInventoryRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<MaterialInventory>> GetAllAsync(MaterialInventoryLocations? location = null, bool withMaterial = false, bool withProductionLine = false)
    {
        var query = _dbContext.MaterialInventories.Include(mi => mi.Material).AsQueryable();

        if (location != null) query = query.Where(mi => mi.Location == location);
        if (withMaterial) query = query.Include(mi => mi.Material);
        if (withProductionLine) query = query.Include(mi => mi.ProductionLine);

        return await query.ToListAsync();
    }

    public async Task<List<MaterialInventory>> GetAllByConditionAsync(Expression<Func<MaterialInventory, bool>> predicate)
    {
        return await _dbContext.MaterialInventories.Where(predicate).Include(mi => mi.Material).ToListAsync();
    }




    public async Task<MaterialInventory?> GetAsync(int? id = null, int? materialId = null, MaterialInventoryLocations? location = null, int? productionLineId = null)
    {
        var query = _dbContext.MaterialInventories.AsQueryable();

        if (id != null) query = query.Where(mi => mi.Id == id);
        if (materialId != null) query = query.Where(mi => mi.MaterialId == materialId);
        if (location != null) query = query.Where(mi => mi.Location == location);
        if (productionLineId != null) query = query.Where(mi => mi.ProductionLineId == productionLineId);

        return await query.FirstOrDefaultAsync();
    }

    public async Task<MaterialInventory?> GetByConditionAsync(Expression<Func<MaterialInventory, bool>> predicate)
    {
        return await _dbContext.MaterialInventories.Where(predicate).FirstOrDefaultAsync();
    }


    public async Task StoreAsync(MaterialInventory materialInventory)
    {
        _dbContext.MaterialInventories.Add(materialInventory);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(MaterialInventory materialInventory)
    {
        _dbContext.MaterialInventories.Update(materialInventory);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateManyAsync(List<MaterialInventory> materialInventories)
    {
        _dbContext.MaterialInventories.UpdateRange(materialInventories);
        await _dbContext.SaveChangesAsync();
    }


    public async Task DeleteAsync(int id)
    {
        var material = _dbContext.MaterialInventories.FirstOrDefaultAsync(mi => mi.Id == id);
        if (material != null)
        {
            _dbContext.Remove(material);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task SaveChangesAsync(List<MaterialTransaction> materialTransactions, List<MaterialInventory> materialInventories)
    {
        Console.WriteLine("dsfdkf");
        using var transaction = await _dbContext.Database.BeginTransactionAsync();

        try
        {
            if (materialTransactions.Any()) await _dbContext.MaterialTransactions.AddRangeAsync(materialTransactions);

            if (materialInventories.Any()) await _dbContext.MaterialInventories.AddRangeAsync(materialInventories);

            await _dbContext.SaveChangesAsync();

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }


}