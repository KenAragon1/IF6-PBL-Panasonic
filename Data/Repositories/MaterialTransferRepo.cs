using Microsoft.EntityFrameworkCore;
using panasonic.Models;
using panasonic.ViewModels.StoreViewModel;

namespace panasonic.Repositories;



public interface IMaterialTransferRepo
{
    Task SendMaterialToPreperationRoom(List<SendMaterialForm> viewModels);
}

public class MaterialTransferRepo : IMaterialTransferRepo
{
    private readonly ApplicationDbContext _dbContext;

    public MaterialTransferRepo(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SendMaterialToPreperationRoom(List<SendMaterialForm> viewModels)
    {
        var newMaterialInventoriesInPreperationRoom = new List<MaterialInventory>();
        var materialTransactions = new List<MaterialTransaction>();

        foreach (var material in viewModels)
        {
            // Update the material inventory in the store
            var materialInventoryInStore = _dbContext.ChangeTracker.Entries<MaterialInventory>().FirstOrDefault(e => e.Entity.Id == material.MaterialInventoryId)?.Entity ?? await getMaterialInventoriesAsync(material.MaterialInventoryId);

            if (materialInventoryInStore == null)
            {
                throw new InvalidOperationException($"Material with ID {material.MaterialInventoryId} not found.");
            }

            materialTransactions.Add(new MaterialTransaction { MaterialId = materialInventoryInStore.MaterialId, Quantity = material.Quantity, Type = TransactionTypes.Send });

            if (_dbContext.Entry(materialInventoryInStore).State == EntityState.Detached)
            {
                _dbContext.MaterialInventories.Attach(materialInventoryInStore);
            }

            materialInventoryInStore.Quantity -= material.Quantity;


            // Create or update material invenotory in the preperation room
            var materialInventoryInPreperationRoom = _dbContext.ChangeTracker.Entries<MaterialInventory>().FirstOrDefault(e => e.Entity.MaterialId == materialInventoryInStore.MaterialId && e.Entity.Location == MaterialInventoryLocations.PreperationRoom)?.Entity ?? await getMaterialInventoriesAsync(location: MaterialInventoryLocations.PreperationRoom, materialId: materialInventoryInStore.MaterialId);
            if (materialInventoryInPreperationRoom != null)
            {
                if (_dbContext.Entry(materialInventoryInPreperationRoom).State == EntityState.Detached)
                {
                    _dbContext.MaterialInventories.Attach(materialInventoryInPreperationRoom);
                }
                materialInventoryInPreperationRoom.Quantity += material.Quantity;
            }
            else
            {
                newMaterialInventoriesInPreperationRoom.Add(new MaterialInventory
                {
                    MaterialId = materialInventoryInStore.MaterialId,
                    Quantity = material.Quantity,
                    Location = MaterialInventoryLocations.PreperationRoom
                });
            }
        }

        if (newMaterialInventoriesInPreperationRoom.Any())
        {
            await _dbContext.MaterialInventories.AddRangeAsync(newMaterialInventoriesInPreperationRoom);
        }

        await _dbContext.MaterialTransactions.AddRangeAsync(materialTransactions);

        await _dbContext.SaveChangesAsync();
    }

    private async Task<MaterialInventory?> getMaterialInventoriesAsync(int? id = null, int? materialId = null, MaterialInventoryLocations? location = null)
    {
        var query = _dbContext.MaterialInventories.Include(mi => mi.Material).AsQueryable();
        if (id != null) query = query.Where(mi => mi.Id == id);
        if (materialId != null) query = query.Where(mi => mi.MaterialId == materialId);
        if (location != null) query = query.Where(mi => mi.Location == location);
        return await query.FirstOrDefaultAsync();
    }

}