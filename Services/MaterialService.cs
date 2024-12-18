using panasonic.Errors;
using panasonic.Exceptions;
using panasonic.Models;
using panasonic.Repositories;
using panasonic.ViewModels.MaterialViewModel;

namespace panasonic.Services;

public interface IMaterialService
{
    MaterialViewModel MaterialViewModel(Material? material);
    Task<List<Material>> GetAllAsync();
    Task<Material> GetByIdAsync(int id);
    Task CreateAsync(MaterialViewModel materialViewModel);
    Task UpdateAsync(MaterialViewModel materialViewModel);
    Task DeleteAsync(int id);


}

public class MaterialService : IMaterialService
{
    private readonly IMaterialRepository _materialRepository;

    public MaterialService(IMaterialRepository materialRepository)
    {
        _materialRepository = materialRepository;

    }

    public async Task<List<Material>> GetAllAsync()
    {
        return await _materialRepository.GetAllAsync();
    }

    public async Task<Material> GetByIdAsync(int id)
    {
        var material = await _materialRepository.GetByIdAsync(id);

        if (material == null) throw new ItemNotFoundException("Material not found");

        return material;
    }
    public async Task CreateAsync(MaterialViewModel materialViewModel)
    {
        try
        {
            var material = new Material
            {
                Name = materialViewModel.Name,
                UnitMeasurement = materialViewModel.UnitMeasurement,
                Number = materialViewModel.Number!.Value,
                DetailMeasurement = materialViewModel.DetailMeasurement,
                DetailQuantity = materialViewModel.DetailQuantity!.Value
            };

            await _materialRepository.StoreAsync(material);
        }
        catch (ExceptionWithType e)
        {
            if (e.Type == ExceptionTypes.UniqueColumn)
            {
                throw new ExceptionWithModelError(nameof(materialViewModel.Number), "This material number has already taken");
            }
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public async Task DeleteAsync(int id)
    {
        try
        {
            await _materialRepository.DeleteAsync(id);
        }
        catch (ItemNotFoundException)
        {
            throw;
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public MaterialViewModel MaterialViewModel(Material? material)
    {
        var viewModel = new MaterialViewModel();
        if (material != null)
        {
            viewModel.Id = material.Id;
            viewModel.Name = material.Name;
            viewModel.Number = material.Number;
            viewModel.DetailMeasurement = material.DetailMeasurement;
            viewModel.DetailQuantity = material.DetailQuantity;
            viewModel.UnitMeasurement = material.UnitMeasurement;
        }
        return viewModel;
    }

    public async Task UpdateAsync(MaterialViewModel materialViewModel)
    {
        var material = await _materialRepository.GetByIdAsync(materialViewModel.Id);

        if (material == null) throw new ItemNotFoundException("This material does not exist");

        material.Name = materialViewModel.Name;
        material.Number = materialViewModel.Number!.Value;
        material.UnitMeasurement = materialViewModel.UnitMeasurement;
        material.DetailMeasurement = material.DetailMeasurement;
        material.DetailQuantity = materialViewModel.DetailQuantity!.Value;

        await _materialRepository.UpdateAsync(material);
    }
}