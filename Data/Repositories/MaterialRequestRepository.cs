using Microsoft.EntityFrameworkCore;
using panasonic.Models;

namespace panasonic.Repositories;

public interface IMaterialRequestRepository
{
    Task<List<MaterialRequest>> GetAllAsync(string? status = null);
    Task<MaterialRequest> GetAsync(int id);
    Task StoreAsync(MaterialRequest materialRequest);
    Task UpdateAsync(MaterialRequest materialRequest);

    Task DeleteAsync(int id);

    IQueryable<MaterialRequest> Query();
}

public class MaterialRequestRepository : IMaterialRequestRepository
{
    private readonly ApplicationDbContext _dbContext;

    public MaterialRequestRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<MaterialRequest>> GetAllAsync(string? status = null)
    {
        var query = _dbContext.MaterialRequests.Include(mr => mr.Material).Include(mr => mr.RequestedBy).AsQueryable();
        if (!string.IsNullOrEmpty(status))
        {
            Enum.TryParse(status, out MaterialRequestStatus requestStatus);
            query = query.Where(mr => mr.Status == requestStatus);
        }
        return await query.ToListAsync();
    }

    public async Task<MaterialRequest> GetAsync(int id)
    {
        return await _dbContext.MaterialRequests.FirstAsync(mr => mr.Id == id);
    }

    public IQueryable<MaterialRequest> Query()
    {
        return _dbContext.MaterialRequests.AsQueryable();
    }

    public async Task StoreAsync(MaterialRequest materialRequest)
    {
        await _dbContext.MaterialRequests.AddAsync(materialRequest);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(MaterialRequest materialRequest)
    {
        _dbContext.MaterialRequests.Update(materialRequest);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var request = await _dbContext.MaterialRequests.FirstAsync(mr => mr.Id == id);
        _dbContext.MaterialRequests.Remove(request);
        await _dbContext.SaveChangesAsync();
    }
}