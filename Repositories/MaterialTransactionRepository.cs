using Microsoft.EntityFrameworkCore;
using panasonic.Models;

namespace panasonic.Repositories;

public interface IMaterialTransactionRepository
{
    Task<List<MaterialTransaction>> GetAllAsync();
    Task<MaterialTransaction?> GetByIdAsync(int id);
}

public class MaterialTransactionRepository : IMaterialTransactionRepository
{
    private ApplicationDbContext _dbContext;

    public MaterialTransactionRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<MaterialTransaction>> GetAllAsync()
    {
        return await _dbContext.MaterialTransactions.Include(mt => mt.MaterialTransactionDetails).ThenInclude(mtd => mtd.Material).Include(mt => mt.User).Include(mt => mt.ProductionLine).ToListAsync();
    }

    public async Task<MaterialTransaction?> GetByIdAsync(int id)
    {
        return await _dbContext.MaterialTransactions.Include(mt => mt.MaterialTransactionDetails).ThenInclude(mtd => mtd.Material).Include(mt => mt.User).Include(mt => mt.ProductionLine).FirstOrDefaultAsync(mt => mt.Id == id);
    }
}