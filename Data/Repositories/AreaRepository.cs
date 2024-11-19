using Microsoft.EntityFrameworkCore;
using panasonic.Models;

namespace panasonic.Repositories;

public interface IAreaRepository
{
    Task<List<Area>> GetAreasAsync(string? AreaType = null);
    Task<Area> GetAreaAsync(int areaId, bool withUser = false);
    Task Create(Area area);
}

public class AreaRepository : IAreaRepository
{
    private readonly ApplicationDbContext _dbContext;

    public AreaRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<List<Area>> GetAreasAsync(string? areaType = null)
    {
        var query = _dbContext.Areas.Include(a => a.AreaType).AsQueryable();
        if (areaType != null) query = query.Where(a => a.AreaType.Type == areaType);

        return await query.ToListAsync();
    }

    public async Task<Area> GetAreaAsync(int areaId, bool withUser = false)
    {
        var query = _dbContext.Areas.Include(a => a.AreaType).AsQueryable();

        if (withUser) query = query.Include(a => a.Users).ThenInclude(u => u.Role);

        return await query.FirstOrDefaultAsync(a => a.Id == areaId);
    }

    public async Task Create(Area area)
    {
        await _dbContext.Areas.AddAsync(area);
        await _dbContext.SaveChangesAsync();
    }

}