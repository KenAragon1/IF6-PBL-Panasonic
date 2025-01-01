using panasonic.Repositories;
using panasonic.ViewModels.MaterialInventoryViewModel;
using panasonic.Models;
using panasonic.Exceptions;
using panasonic.Errors;
using panasonic.Helpers;
using System.Linq.Expressions;
using panasonic.ViewModels.MaterialInventoryViewModels;

namespace panasonic.Services;

public interface IMaterialInventoryService
{
    Task<List<MaterialInventory>> GetAllAsync(Expression<Func<MaterialInventory, bool>>? predicate = null);
    Task SendMaterialAsync(SendViewModel sendViewModel);
    Task PickupMaterial(PickupInventoryViewModel pickupInventoryViewModel);
    Task ReturnMaterialAsync(ReturnInventoryViewModel returnInventoryViewModel);
    Task UseMaterialAsync(UseInventoryViewModel useInventoryViewModel);
}

public class MaterialInventoryService : IMaterialInventoryService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMaterialInventoryRepository _materialInventoryRepository;
    private readonly IMaterialRequestRepository _materialRequestRepository;
    private readonly IUserClaimHelper _userClaimHelper;

    public MaterialInventoryService(ApplicationDbContext dbContext, IMaterialInventoryRepository materialInventoryRepository, IMaterialRequestRepository materialRequestRepository, IUserClaimHelper userClaimHelper)
    {
        _dbContext = dbContext;
        _materialInventoryRepository = materialInventoryRepository;
        _materialRequestRepository = materialRequestRepository;
        _userClaimHelper = userClaimHelper;
    }

    public async Task<List<MaterialInventory>> GetAllAsync(Expression<Func<MaterialInventory, bool>>? predicate = null)
    {
        return await _materialInventoryRepository.GetAllAsync(predicate);
    }

    public async Task PickupMaterial(PickupInventoryViewModel pickupInventoryViewModel)
    {
        int.TryParse(_userClaimHelper.GetUserClaim("UserId"), out int userId);

        var newMaterialTransaction = new MaterialTransaction
        {
            Type = TransactionTypes.Pickup,
            ProductionLineId = pickupInventoryViewModel.ProductionLineId,
            UserId = userId
        };
        var newMaterialInventoryInProductionLine = new List<MaterialInventory>();

        foreach (var (form, index) in pickupInventoryViewModel.InventoryForms.Select((value, i) => (value, i)))
        {
            var materialInPreperationRoom = await _materialInventoryRepository.GetAsync(form.InventoryId);

            if (materialInPreperationRoom == null) throw new ExceptionWithModelError($"Forms[{index}].MaterialInventoryId", $"Material Inventory with ID {form.InventoryId} not found.");

            newMaterialTransaction.MaterialTransactionDetails.Add(new MaterialTransactionDetail
            {
                MaterialId = materialInPreperationRoom.MaterialId,
                Quantity = form.Quantity
            });

            await MoveInventory(form.Quantity, materialInPreperationRoom, MaterialInventoryLocations.ProductionLine, pickupInventoryViewModel.ProductionLineId);

        }

        await _materialInventoryRepository.SaveChangesAsync(newMaterialTransaction, newMaterialInventoryInProductionLine);
    }



    public async Task SendMaterialAsync(SendViewModel sendViewModel)
    {
        int.TryParse(_userClaimHelper.GetUserClaim("UserId"), out int userId);
        var newMaterialTransactions = new MaterialTransaction { Type = TransactionTypes.Send, UserId = userId };
        var ListOfNewMaterialInventoriesInPreperationRoom = new List<MaterialInventory>();



        foreach (var form in sendViewModel.Forms)
        {
            var materialRequest = await _materialRequestRepository.GetAsync(id: form.MaterialRequestId);

            if (materialRequest.Status != MaterialRequestStatus.Approved) throw new OperationNotAllowed("Material request status must be approved first before can be handled");

            var materialQuantity = form.Measurement switch
            {
                ("Unit") => form.QuantitySend * materialRequest.Material!.DetailQuantity,
                ("Detail") => form.QuantitySend,
                _ => form.QuantitySend,
            };

            newMaterialTransactions.MaterialTransactionDetails.Where(mtd => mtd.MaterialId == materialRequest.MaterialId).FirstOrDefault();

            var newMaterialTransactionDetail = new MaterialTransactionDetail
            {
                MaterialId = materialRequest.MaterialId,
                Quantity = materialQuantity,
            };

            newMaterialTransactions.MaterialTransactionDetails.Add(newMaterialTransactionDetail);

            // Modify or add new material inventory in preperation room
            var materialInventoryInPreperationRoom = await _materialInventoryRepository
            .GetAsync(location: MaterialInventoryLocations.PreperationRoom, materialId: materialRequest.MaterialId)
            ?? _dbContext.ChangeTracker.Entries<MaterialInventory>()
            .FirstOrDefault(e => e.Entity.Location == MaterialInventoryLocations.PreperationRoom && e.Entity.MaterialId == materialRequest.MaterialId)?
            .Entity;

            if (materialInventoryInPreperationRoom != null)
            {
                materialInventoryInPreperationRoom.Quantity += materialQuantity;
            }
            else
            {
                var newMaterialInventoryInPreperationRoom = new MaterialInventory
                {
                    Location = MaterialInventoryLocations.PreperationRoom,
                    MaterialId = materialRequest.MaterialId,
                    Quantity = materialQuantity
                };
                ListOfNewMaterialInventoriesInPreperationRoom.Add(newMaterialInventoryInPreperationRoom);
            }

            // Edit Fullfilled quantity in material request
            materialRequest.FullfilledQuantity += form.QuantitySend;

        }

        await _materialInventoryRepository.SaveChangesAsync(newMaterialTransactions, ListOfNewMaterialInventoriesInPreperationRoom);
    }

    public async Task ReturnMaterialAsync(ReturnInventoryViewModel returnInventoryViewModel)
    {
        int.TryParse(_userClaimHelper.GetUserClaim("UserId"), out int userId);

        var newMaterialTransactions = new MaterialTransaction { Type = TransactionTypes.Return, ProductionLineId = returnInventoryViewModel.ProductionLineId, UserId = userId };
        var ListOfNewMaterialInventoriesInPreperationRoom = new List<MaterialInventory>();

        foreach (var (form, index) in returnInventoryViewModel.InventoryForms.Select((value, index) => (value, index)))
        {
            var materialInProductionLine = await _materialInventoryRepository.GetAsync(form.InventoryId);

            if (materialInProductionLine == null) throw new ItemNotFoundException("Material Inventory Not Found");

            if (materialInProductionLine.Location != MaterialInventoryLocations.ProductionLine) throw new OperationNotAllowed("Can't return material inventory that's already in preperation room");

            var newMaterialTransactionDetail = new MaterialTransactionDetail
            {
                MaterialId = materialInProductionLine.MaterialId,
                Quantity = form.Quantity,
            };

            newMaterialTransactions.MaterialTransactionDetails.Add(newMaterialTransactionDetail);

            if (materialInProductionLine.Quantity < form.Quantity) throw new ExceptionWithModelError($"Forms[{index}].Quantity", "Cannot return material more than what's available");

            await MoveInventory(form.Quantity, materialInProductionLine, MaterialInventoryLocations.PreperationRoom, null);


        }
        await _materialInventoryRepository.SaveChangesAsync(newMaterialTransactions, ListOfNewMaterialInventoriesInPreperationRoom);

    }

    public async Task UseMaterialAsync(UseInventoryViewModel useInventoryViewModel)
    {
        int.TryParse(_userClaimHelper.GetUserClaim("UserId"), out int userId);

        var newMaterialTransactions = new MaterialTransaction { Type = TransactionTypes.Production, ProductionLineId = useInventoryViewModel.ProductionLineId, UserId = userId };

        foreach (var (form, index) in useInventoryViewModel.InventoryForms.Select((value, index) => (value, index)))
        {
            var materialInventoryInProductionLine = await _materialInventoryRepository.GetAsync(form.InventoryId);

            if (materialInventoryInProductionLine == null) throw new ExceptionWithModelError($"Forms[{index}].MaterialInventoryId", "Material Inventory not found");
            if (materialInventoryInProductionLine.Location != MaterialInventoryLocations.ProductionLine) throw new ExceptionWithModelError($"Forms[{index}].MaterialInventoryId", "This material isnt in the production line");
            if (form.Quantity > materialInventoryInProductionLine.Quantity) throw new ExceptionWithModelError($"Forms[{index}].Quantity", "Cant use that many quantity since its more than what is available");

            materialInventoryInProductionLine.Quantity -= form.Quantity;

            newMaterialTransactions.MaterialTransactionDetails.Add(new MaterialTransactionDetail
            {
                MaterialId = materialInventoryInProductionLine.MaterialId,
                Quantity = form.Quantity,
            });
        }

        await _materialInventoryRepository.SaveChangesAsync(newMaterialTransactions);
    }



    private async Task MoveInventory(int quantityToBeMoved, MaterialInventory inventoryToMove, MaterialInventoryLocations destination, int? LineId)
    {
        if (destination == MaterialInventoryLocations.ProductionLine && LineId == null) throw new InvalidOperationException();

        var inventoryDestination = await _materialInventoryRepository
        .GetByConditionAsync(mi => mi.MaterialId == inventoryToMove.MaterialId && mi.Location == destination && mi.ProductionLineId == LineId)
        ?? new MaterialInventory
        {
            MaterialId = inventoryToMove.MaterialId,
            Quantity = 0,
            Location = destination,
            ProductionLineId = LineId
        };

        inventoryToMove.Quantity -= quantityToBeMoved;
        inventoryDestination.Quantity += quantityToBeMoved;

        // id 0 means the inventory is created, not updated. When creating a new invenotry class, the id's default value is 0
        if (inventoryDestination.Id == 0) _dbContext.MaterialInventories.Add(inventoryDestination);


    }
}