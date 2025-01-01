using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using panasonic.Exceptions;
using panasonic.Models;

namespace panasonic.Repositories;

public interface IMaterialRepository
{
    Task<List<Material>> GetAllAsync();
    Task<Material?> GetByIdAsync(int id);
    Task StoreAsync(Material material);
    Task UpdateAsync(Material material);
    Task DeleteAsync(int id);

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
        return await _dbContext.Materials.Where(m => !m.IsDeleted).AsNoTracking().ToListAsync();
    }

    public async Task<Material?> GetByIdAsync(int id)
    {
        var material = await _dbContext.Materials.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

        if (material != null && !material.IsDeleted) return material;

        return null;
    }

    public async Task StoreAsync(Material material)
    {
        if (await IsNumberTaken(material.Number)) throw new ExceptionWithType(ExceptionTypes.UniqueColumn, "number has already taken");

        _dbContext.Materials.Add(material);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Material material)
    {
        _dbContext.Update(material);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var material = await GetByIdAsync(id);

        if (material == null) throw new ItemNotFoundException("Item Not Found");

        material.IsDeleted = true;

        await UpdateAsync(material);
    }



    private async Task<bool> IsNumberTaken(int number)
    {
        return await _dbContext.Materials.AnyAsync(m => m.Number == number);
    }
}