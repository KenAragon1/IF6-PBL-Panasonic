using panasonic.Repositories;
using panasonic.ViewModels.MaterialInventoryViewModel;
using panasonic.Models;
using panasonic.Exceptions;
using panasonic.Errors;
using panasonic.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using panasonic.Dtos;

namespace panasonic.Services;

public interface IMaterialInventoryService
{
    Task<PickupViewModel> PickupViewModel();
    Task<SendViewModel> CreateSendViewModelAsync(List<SendForm>? sendForms = null);
    Task<ReturnViewModel> ReturnViewModelAsync(ReturnViewModel? returnViewModel = null);
    Task<UseViewModel> UseViewModelAsync(UseViewModel? useViewModel = null);
    Task SendMaterialAsync(SendViewModel sendViewModel);
    Task PickupMaterial(int lineDestination, List<MaterialInventoryForm> inventoryForms);
    Task ReturnMaterialAsync(ReturnViewModel returnViewModel);
    Task UseMaterialAsync(UseViewModel useViewModel);
}

public class MaterialInventoryService : IMaterialInventoryService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMaterialInventoryRepository _materialInventoryRepository;
    private readonly IMaterialRequestRepository _materialRequestRepository;
    private readonly IProductionLineRepository _productionLineRepository;
    private readonly IUserClaimHelper _userClaimHelper;
    private readonly IMaterialRepository _materialRepository;

    public MaterialInventoryService(ApplicationDbContext dbContext, IMaterialRepository materialRepository, IMaterialInventoryRepository materialInventoryRepository, IMaterialRequestRepository materialRequestRepository, IProductionLineRepository productionLineRepository, IUserClaimHelper userClaimHelper)
    {
        _dbContext = dbContext;
        _materialInventoryRepository = materialInventoryRepository;
        _materialRequestRepository = materialRequestRepository;
        _productionLineRepository = productionLineRepository;
        _userClaimHelper = userClaimHelper;
        _materialRepository = materialRepository;
    }

    public async Task<PickupViewModel> PickupViewModel()
    {
        var viewModel = new PickupViewModel
        {
            ProductionLineOptions = await _productionLineRepository.GetAllAsync(),
            Materials = await _materialRepository.GetAllWithInventoryByCondition(mi => mi.Location == MaterialInventoryLocations.PreperationRoom)
        };

        return viewModel;
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

    public async Task<ReturnViewModel> ReturnViewModelAsync(ReturnViewModel? returnViewModel = null)
    {
        var viewModel = new ReturnViewModel
        {
            MaterialInventories = await _materialInventoryRepository.GetAllByConditionAsync(mi => mi.Location == MaterialInventoryLocations.ProductionLine && mi.Quantity > 0),
            ProductionLines = await _productionLineRepository.GetAllAsync()
        };

        if (returnViewModel != null)
        {
            viewModel.ProductionLineId = returnViewModel.ProductionLineId;
            if (returnViewModel.Forms != null) viewModel.Forms = returnViewModel.Forms;
        }

        return viewModel;
    }
    public async Task<UseViewModel> UseViewModelAsync(UseViewModel? useViewModel = null)
    {
        var viewModel = new UseViewModel
        {
            MaterialInventories = await _materialInventoryRepository.GetAllByConditionAsync(mi => mi.Location == MaterialInventoryLocations.ProductionLine && mi.Quantity > 0),
            ProductionLines = await _productionLineRepository.GetAllAsync()
        };

        if (useViewModel != null)
        {
            viewModel.ProductionLineId = useViewModel.ProductionLineId;
            if (useViewModel.Forms != null) viewModel.Forms = useViewModel.Forms;
        }

        return viewModel;
    }


    public async Task PickupMaterial(int lineDestination, List<MaterialInventoryForm> inventoryForms)
    {
        int.TryParse(_userClaimHelper.GetUserClaim("UserId"), out int userId);

        var newMaterialTransaction = new MaterialTransaction
        {
            Type = TransactionTypes.Pickup,
            ProductionLineId = lineDestination,
            UserId = userId
        };
        var newMaterialInventoryInProductionLine = new List<MaterialInventory>();

        foreach (var (form, index) in inventoryForms.Select((value, i) => (value, i)))
        {
            var materialInPreperationRoom = await _materialInventoryRepository.GetAsync(form.MaterialInventoryId);

            if (materialInPreperationRoom == null) throw new ExceptionWithModelError($"Forms[{index}].MaterialInventoryId", $"Material Inventory with ID {form.MaterialInventoryId} not found.");

            var materialTransactionDetail = newMaterialTransaction.MaterialTransactionDetails
            .Where(mtd => mtd.MaterialId == materialInPreperationRoom.MaterialId)
            .FirstOrDefault();

            if (materialTransactionDetail != null)
            {
                materialTransactionDetail.Quantity += form.Quantity;
            }
            else
            {
                newMaterialTransaction.MaterialTransactionDetails.Add(new MaterialTransactionDetail
                {
                    MaterialId = materialInPreperationRoom.MaterialId,
                    Quantity = form.Quantity
                });
            }


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

    public async Task ReturnMaterialAsync(ReturnViewModel returnViewModel)
    {
        int.TryParse(_userClaimHelper.GetUserClaim("UserId"), out int userId);

        var newMaterialTransactions = new MaterialTransaction { Type = TransactionTypes.Return, ProductionLineId = returnViewModel.ProductionLineId, UserId = userId };
        var ListOfNewMaterialInventoriesInPreperationRoom = new List<MaterialInventory>();

        foreach (var (form, index) in returnViewModel.Forms.Select((value, index) => (value, index)))
        {
            var materialInProductionLine = await _materialInventoryRepository.GetAsync(form.MaterialInventoryId);

            if (materialInProductionLine == null) throw new ItemNotFoundException("Material Inventory Not Found");

            if (materialInProductionLine.Location != MaterialInventoryLocations.ProductionLine) throw new OperationNotAllowed("Can't return material inventory that's already in preperation room");

            var newMaterialTransactionDetail = new MaterialTransactionDetail
            {
                MaterialId = materialInProductionLine.MaterialId,
                Quantity = form.Quantity,
            };

            newMaterialTransactions.MaterialTransactionDetails.Add(newMaterialTransactionDetail);

            if (materialInProductionLine.Quantity < form.Quantity) throw new ExceptionWithModelError($"Forms[{index}].Quantity", "Cannot return material more than what's available");

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
        await _materialInventoryRepository.SaveChangesAsync(newMaterialTransactions, ListOfNewMaterialInventoriesInPreperationRoom);

    }

    public async Task UseMaterialAsync(UseViewModel useViewModel)
    {
        int.TryParse(_userClaimHelper.GetUserClaim("UserId"), out int userId);

        var newMaterialTransactions = new MaterialTransaction { Type = TransactionTypes.Production, ProductionLineId = useViewModel.ProductionLineId, UserId = userId };

        foreach (var (form, index) in useViewModel.Forms.Select((value, index) => (value, index)))
        {
            var materialInventoryInProductionLine = await _materialInventoryRepository.GetAsync(form.MaterialInventoryId);

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
}