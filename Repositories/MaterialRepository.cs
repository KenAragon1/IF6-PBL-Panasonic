using Microsoft.EntityFrameworkCore;
using panasonic.Models;

namespace panasonic.Repositories;

public interface IMaterialRepository
{
    Task<List<Material>> GetAllAsync();
    Task<Material> GetAsync(int id);
    Task StoreAsync(Material material);
    Task UpdateAsync(Material material);
    Task DeleteAsync(Material material);

}

public class MaterialRepository : IMaterialRepository
{
    private readonly ApplicationDbContext _dbContext;

    public MaterialRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }



    public async Task<List<Material>> GetAllAsync()
    {
        return await _dbContext.Materials.ToListAsync();
    }

    public async Task<Material> GetAsync(int id)
    {
        return await _dbContext.Materials.FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task StoreAsync(Material material)
    {
        _dbContext.Materials.Add(material);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Material material)
    {
        _dbContext.Update(material);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Material material)
    {
        _dbContext.Materials.Remove(material);
        await _dbContext.SaveChangesAsync();
    }
}