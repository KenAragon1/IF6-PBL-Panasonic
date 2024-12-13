using panasonic.Repositories;
using panasonic.ViewModels.MaterialInventoryViewModel;
using panasonic.Models;
using panasonic.Exceptions;
using panasonic.Errors;

namespace panasonic.Services;

public interface IMaterialInventoryService
{
    Task<SendViewModel> CreateSendViewModelAsync(List<SendForm>? sendForms = null);
    Task<ReturnViewModel> ReturnViewModelAsync(List<MaterialInventoryForm>? materialInventoryForms = null);
    Task SendMaterialAsync(SendViewModel sendViewModel);
    Task PickupMaterial(int lineDestination, List<MaterialInventoryForm> inventoryForms);
    Task ReturnMaterialAsync(List<MaterialInventoryForm> materialInventoryForms);
    Task UseMaterial(List<MaterialInventoryForm> materialInventoryForms);
}

public class MaterialInventoryService : IMaterialInventoryService
{
    private readonly IMaterialInventoryRepository _materialInventoryRepository;
    private readonly IMaterialRequestRepository _materialRequestRepository;
    private readonly IProductionLineRepository _productionLineRepository;

    public MaterialInventoryService(IMaterialInventoryRepository materialInventoryRepository, IMaterialRequestRepository materialRequestRepository, IProductionLineRepository productionLineRepository)
    {
        _materialInventoryRepository = materialInventoryRepository;
        _materialRequestRepository = materialRequestRepository;
        _productionLineRepository = productionLineRepository;
    }

    public async Task<SendViewModel> CreateSendViewModelAsync(List<SendForm>? sendForms = null)
    {
        var viewModel = new SendViewModel
        {
            MaterialRequests = await _materialRequestRepository.GetAllByCondition(mr => mr.Status == MaterialRequestStatus.Approved && mr.FullfilledQuantity < mr.RequestedQuantity)
        };
        if (sendForms != null) viewModel.Forms = sendForms;
        return viewModel;
    }

    public async Task<ReturnViewModel> ReturnViewModelAsync(List<MaterialInventoryForm>? materialInventoryForms = null)
    {
        var viewModel = new ReturnViewModel
        {
            MaterialInventories = await _materialInventoryRepository.GetAllByConditionAsync(mi => mi.Location == MaterialInventoryLocations.ProductionLine && mi.Quantity > 0),
            ProductionLines = await _productionLineRepository.GetAllAsync()
        };

        if (materialInventoryForms != null) viewModel.Forms = materialInventoryForms;

        return viewModel;
    }


    public async Task PickupMaterial(int lineDestination, List<MaterialInventoryForm> inventoryForms)
    {
        var newMaterialTransaction = new List<MaterialTransaction>();
        var newMaterialInventoryInProductionLine = new List<MaterialInventory>();

        foreach (var (form, index) in inventoryForms.Select((value, i) => (value, i)))
        {
            var materialInPreperationRoom = await _materialInventoryRepository.GetAsync(form.MaterialInventoryId);

            if (materialInPreperationRoom == null) throw new ExceptionWithModelError($"Forms[{index}].MaterialInventoryId", $"Material Inventory with ID {form.MaterialInventoryId} not found.");

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



    public async Task SendMaterialAsync(SendViewModel sendViewModel)
    {
        var ListOfNewMaterialTransactions = new List<MaterialTransaction>();
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

            var newMaterialTransaction = new MaterialTransaction
            {
                MaterialId = materialRequest.MaterialId,
                Quantity = materialQuantity,
                Type = TransactionTypes.Send,
                ProductionLineId = materialRequest.ProductionLineId
            };

            ListOfNewMaterialTransactions.Add(newMaterialTransaction);



            // Modify or add new material inventory in preperation room
            var materialInventoryInPreperationRoom = await _materialInventoryRepository.GetAsync(location: MaterialInventoryLocations.PreperationRoom, materialId: newMaterialTransaction.MaterialId);



            if (materialInventoryInPreperationRoom != null)
            {
                materialInventoryInPreperationRoom.Quantity += materialQuantity;
            }
            else
            {
                var newMaterialInventoryInPreperationRoom = new MaterialInventory
                {
                    Location = MaterialInventoryLocations.PreperationRoom,
                    MaterialId = newMaterialTransaction.MaterialId,
                    Quantity = materialQuantity
                };
                ListOfNewMaterialInventoriesInPreperationRoom.Add(newMaterialInventoryInPreperationRoom);
            }

            // Edit Fullfilled quantity in material request
            materialRequest.FullfilledQuantity += newMaterialTransaction.Quantity;

        }

        await _materialInventoryRepository.SaveChangesAsync(ListOfNewMaterialTransactions, ListOfNewMaterialInventoriesInPreperationRoom);
    }

    public async Task ReturnMaterialAsync(List<MaterialInventoryForm> materialInventoryForms)
    {
        var ListOfNewMaterialTransactions = new List<MaterialTransaction>();
        var ListOfNewMaterialInventoriesInPreperationRoom = new List<MaterialInventory>();

        foreach (var form in materialInventoryForms)
        {
            var materialInProductionLine = await _materialInventoryRepository.GetAsync(form.MaterialInventoryId);

            if (materialInProductionLine == null) throw new ItemNotFoundException("Material Inventory Not Found");

            if (materialInProductionLine.Location != MaterialInventoryLocations.ProductionLine) throw new OperationNotAllowed("Can't return material inventory that's already in preperation room");

            var newMaterialTransaction = new MaterialTransaction
            {
                MaterialId = materialInProductionLine.MaterialId,
                Quantity = form.Quantity,
                Type = TransactionTypes.Return,
                ProductionLineId = materialInProductionLine.ProductionLineId
            };

            ListOfNewMaterialTransactions.Add(newMaterialTransaction);

            if (materialInProductionLine.Quantity < form.Quantity) throw new OperationNotAllowed("Cannot return material more than what's available");

            materialInProductionLine.Quantity -= form.Quantity;

            var materialInventoryInPreperationRoom = await _materialInventoryRepository.GetByConditionAsync(mr => mr.Location == MaterialInventoryLocations.PreperationRoom && mr.MaterialId == materialInProductionLine.MaterialId);

            if (materialInventoryInPreperationRoom != null)
            {
                materialInventoryInPreperationRoom.Quantity += form.Quantity;
            }
            else
            {
                var newMaterialInventoryInPreperationRoom = new MaterialInventory
                {
                    MaterialId = materialInProductionLine.MaterialId,
                    Quantity = form.Quantity,
                    Location = MaterialInventoryLocations.PreperationRoom
                };

                ListOfNewMaterialInventoriesInPreperationRoom.Add(newMaterialInventoryInPreperationRoom);
            }


        }
        await _materialInventoryRepository.SaveChangesAsync(ListOfNewMaterialTransactions, ListOfNewMaterialInventoriesInPreperationRoom);

    }

    public async Task UseMaterial(List<MaterialInventoryForm> materialInventoryForms)
    {
        var ListOfNewMaterialTransactions = new List<MaterialTransaction>();

        foreach (var (form, index) in materialInventoryForms.Select((value, index) => (value, index)))
        {
            var materialInventoryInProductionLine = await _materialInventoryRepository.GetAsync(form.MaterialInventoryId);

            if (materialInventoryInProductionLine == null) throw new ExceptionWithModelError($"Forms[{index}].MaterialInventoryId", "Material Inventory not found");
            if (materialInventoryInProductionLine.Location != MaterialInventoryLocations.ProductionLine) throw new ExceptionWithModelError($"Forms[{index}].MaterialInventoryId", "This material isnt in the production line");
            if (form.Quantity > materialInventoryInProductionLine.Quantity) throw new ExceptionWithModelError($"Forms[{index}].Quantity", "Cant use that many quantity since its more than what is available");

            materialInventoryInProductionLine.Quantity -= form.Quantity;

            ListOfNewMaterialTransactions.Add(new MaterialTransaction
            {
                MaterialId = materialInventoryInProductionLine.MaterialId,
                Quantity = form.Quantity,
                Type = TransactionTypes.Production
            });
        }

        await _materialInventoryRepository.SaveChangesAsync(ListOfNewMaterialTransactions);
    }
}