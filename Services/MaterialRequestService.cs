using panasonic.Exceptions;
using panasonic.Helpers;
using panasonic.Models;
using panasonic.Repositories;
using panasonic.ViewModels.MaterialRequestViewModel;


namespace panasonic.Services;



public interface IMaterialRequestService
{
    Task CreateAsync(CreateViewModel viewModel);
    Task VerifyAsync(int requestId);
    Task ApproveAsync(int requestId);
    Task RejectAsync(int requestId);

}

public class MaterialRequestService : IMaterialRequestService
{
    private readonly IMaterialRequestRepository _materialRequestRepository;
    private readonly IUserClaimHelper _userClaimHelper;

    public MaterialRequestService(IMaterialRequestRepository materialRequestRepository, IUserClaimHelper userClaimHelper)
    {
        _materialRequestRepository = materialRequestRepository;
        _userClaimHelper = userClaimHelper;
    }

    public async Task CreateAsync(CreateViewModel createViewModel)
    {
        int.TryParse(_userClaimHelper.GetUserClaim("UserId"), out int userId);

        var newMaterialRequests = createViewModel.CreateForms.Select(f => new MaterialRequest
        {
            MaterialId = f.MaterialId,
            ProductionLineId = f.ProductionLineId,
            RequestedById = userId,
            RequestedQuantity = f.Quantity,
            RequiredAt = f.RequiredAt
        }).ToList();

        await _materialRequestRepository.StoreManyAsync(newMaterialRequests);
    }

    public async Task RejectAsync(int requestId)
    {
        int.TryParse(_userClaimHelper.GetUserClaim("UserId"), out int userId);


        var request = await _materialRequestRepository.GetAsync(id: requestId);

        if (request == null) throw new ItemNotFoundException("Request Not Found");

        request.Reject(userId);

        await _materialRequestRepository.SaveChangesAsync();


    }

    public async Task VerifyAsync(int requestId)
    {
        int.TryParse(_userClaimHelper.GetUserClaim("UserId"), out int userId);

        var request = await _materialRequestRepository.GetAsync(id: requestId);

        if (request == null) throw new ItemNotFoundException("Request Not Found");

        request.Verify(userId);

        await _materialRequestRepository.SaveChangesAsync();
    }

    public async Task ApproveAsync(int requestId)
    {
        int.TryParse(_userClaimHelper.GetUserClaim("UserId"), out int userId);

        var request = await _materialRequestRepository.GetAsync(id: requestId);

        if (request == null) throw new ItemNotFoundException("Request Not Found");

        request.Approve(userId);

        await _materialRequestRepository.SaveChangesAsync();
    }


}