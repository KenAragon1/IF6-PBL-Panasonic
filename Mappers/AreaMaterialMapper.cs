using panasonic.Data.DTOs;
using panasonic.Models;

namespace panasonic.Mappers;

public class AreaMaterialMapper
{
    public List<AreaMaterialDTO> MapToDto(List<AreaMaterial> areaMaterials)
    {
        return areaMaterials.Select(am => new AreaMaterialDTO { Id = am.Id, Name = am.Material!.Name, QrCodeUrl = am.Material.QrCodeUrl, ExpDate = new DateOnly(2024, 11, 22), Remark = 12, Quantity = am.Quantity }).ToList();
    }

    public AreaMaterialDTO MapToDto(AreaMaterial areaMaterial)
    {
        return new AreaMaterialDTO { Id = areaMaterial.Id, Name = areaMaterial.Material!.Name, QrCodeUrl = areaMaterial.Material.QrCodeUrl, Quantity = areaMaterial.Quantity, ExpDate = new DateOnly(2024, 11, 22), Remark = 12 };
    }
}

public class Test
{
    public enum RoleType { Admin, ShiftLeader }
    public required RoleType Role { get; set; }
}

public class Mod
{
    private Test test = new Test { Role = Test.RoleType.Admin };
}