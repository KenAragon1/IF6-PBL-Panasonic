using panasonic.Dtos.MaterialDtos;
using panasonic.Errors;
using panasonic.Exceptions;
using panasonic.Models;
using panasonic.Repositories;
using panasonic.ViewModels.MaterialViewModels;

namespace panasonic.Services;

public interface IMaterialService
{
    Task<List<Material>> GetAllAsync();
    Task<Material> GetByIdAsync(int id);
    Task CreateAsync(CreateMaterialViewModel createMaterialViewModel);
    Task UpdateAsync(int materialId, EditMaterialViewModel editMaterialViewModel);
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
    public async Task CreateAsync(CreateMaterialViewModel createMaterialViewModel)
    {
        try
        {
            var material = new Material
            {
                Name = createMaterialViewModel.MaterialName,
                UnitMeasurement = createMaterialViewModel.UnitMeasurement,
                Number = createMaterialViewModel.MaterialNumber,
                DetailMeasurement = createMaterialViewModel.DetailMeasurement,
                DetailQuantity = createMaterialViewModel.DetailQuantity,
                Barcode = createMaterialViewModel.Barcode
            };

            await _materialRepository.StoreAsync(material);
        }
        catch (ExceptionWithType e)
        {
            if (e.Type == ExceptionTypes.UniqueColumn)
            {
                throw new ExceptionWithModelError(nameof(createMaterialViewModel.MaterialNumber), "This material number has already taken");
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



    public async Task UpdateAsync(int materialId, EditMaterialViewModel editMaterialViewModel)
    {
        var material = await _materialRepository.GetByIdAsync(materialId);


        if (material == null) throw new ItemNotFoundException("This material does not exist");

        material.Name = editMaterialViewModel.MaterialName;
        material.Number = editMaterialViewModel.MaterialNumber;
        material.UnitMeasurement = editMaterialViewModel.UnitMeasurement;
        material.DetailMeasurement = material.DetailMeasurement;
        material.DetailQuantity = editMaterialViewModel.DetailQuantity;
        material.Barcode = editMaterialViewModel.Barcode;

        await _materialRepository.UpdateAsync(material);
    }
}