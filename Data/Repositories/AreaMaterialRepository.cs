using Microsoft.EntityFrameworkCore;
using panasonic.Models;

namespace panasonic.Repositories;

public interface IAreaMaterialRepository
{
    Task<List<Material>> GetMaterialsAsync(int? areaId = null);
}

public class AreaMaterialRepository : IAreaMaterialRepository
{
    private readonly ApplicationDbContext _dbContext;

    public AreaMaterialRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Material>> GetMaterialsAsync(int? areaId = null)
    {
        return await _dbContext.Materials.Include(m => m.AreaMaterials).Where(m => m.AreaMaterials.Any(ms => ms.AreaId == areaId)).ToListAsync();

    }
}