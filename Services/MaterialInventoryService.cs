using panasonic.Repositories;
using panasonic.ViewModels.MaterialInventoryViewModel;
using panasonic.Models;

namespace panasonic.Services;

public interface IMaterialInventoryService
{
    Task SendMaterial();
    Task PickupMaterial(int lineDestination, List<MaterialInventoryForm> inventoryForms);
    Task ReturnMaterial();
    Task UseMaterial();
}

public class MaterialInventoryService : IMaterialInventoryService
{
    private readonly IMaterialInventoryRepository _materialInventoryRepository;

    public MaterialInventoryService(IMaterialInventoryRepository materialInventoryRepository)
    {
        _materialInventoryRepository = materialInventoryRepository;
    }

    public async Task PickupMaterial(int lineDestination, List<MaterialInventoryForm> inventoryForms)
    {
        var newMaterialTransaction = new List<MaterialTransaction>();
        var newMaterialInventoryInProductionLine = new List<MaterialInventory>();

        foreach (var form in inventoryForms)
        {
            var materialInPreperationRoom = await _materialInventoryRepository.GetAsync(form.MaterialInventoryId);

            if (materialInPreperationRoom == null) throw new InvalidOperationException($"Material Inventory with ID {form.MaterialInventoryId} not found.");

            newMaterialTransaction.Add(new MaterialTransaction { MaterialId = materialInPreperationRoom.MaterialId, Quantity = form.Quantity, Type = TransactionTypes.Pickup, ProductionLineId = lineDestination });

            materialInPreperationRoom.Quantity -= form.Quantity;

            var materialInProductionLine = await _materialInventoryRepository.GetAsync(materialId: materialInPreperationRoom.MaterialId, productionLineId: lineDestination);

            if (materialInProductionLine != null)
            {
                materialInProductionLine.Quantity += form.Quantity;
            }
            else
            {
                newMaterialInventoryInProductionLine.Add(new MaterialInventory
                {
                    Location = MaterialInventoryLocations.ProductionLine,
                    MaterialId = materialInPreperationRoom.MaterialId,
                    ProductionLineId = lineDestination,
                    Quantity = form.Quantity
                });
            }
        }

        await _materialInventoryRepository.SaveChangesAsync(newMaterialTransaction, newMaterialInventoryInProductionLine);
    }

    public Task ReturnMaterial()
    {
        throw new NotImplementedException();
    }

    public Task SendMaterial()
    {
        throw new NotImplementedException();
    }

    public Task UseMaterial()
    {
        throw new NotImplementedException();
    }
}