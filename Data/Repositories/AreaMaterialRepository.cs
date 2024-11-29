using Microsoft.EntityFrameworkCore;
using panasonic.Models;

namespace panasonic.Repositories;

public interface IAreaMaterialRepository
{
    Task<List<AreaMaterial>> GetAllAsync(string? areaType = null);
    Task StoreAsync(AreaMaterial areaMaterial);

    Task DeleteAsync(int Id);
}

public class AreaMaterialRepository : IAreaMaterialRepository
{
    private readonly ApplicationDbContext _dbContext;

    public AreaMaterialRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }



    public async Task<List<AreaMaterial>> GetAllAsync(string? areaType = null)
    {
        var query = _dbContext.AreaMaterials.Include(am => am.Area).Include(am => am.Material).AsQueryable();
        if (!string.IsNullOrEmpty(areaType))
        {
            Enum.TryParse(areaType, out AreaTypes type);
            query = query.Where(am => am.Area!.Type == type);

        }
        return await query.ToListAsync();
    }

    public async Task StoreAsync(AreaMaterial areaMaterial)
    {
        await _dbContext.AreaMaterials.AddAsync(areaMaterial);

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var material = await _dbContext.AreaMaterials.FirstOrDefaultAsync(am => am.Id == id);

        if (material != null)
        {
            _dbContext.AreaMaterials.Remove(material);
            await _dbContext.SaveChangesAsync();
        }

    }
}