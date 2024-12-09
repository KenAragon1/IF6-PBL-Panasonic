using System.ComponentModel.DataAnnotations;
using panasonic.Data.DTOs;
using panasonic.Models;

namespace panasonic.ViewModels.StoreViewModel;

public class MaterialViewModel
{
    public required List<MaterialInventory> MaterialInventories;
}

public class AddViewModel
{
    [Required]
    public int MaterialId { get; set; }

    [Required]
    public int Quantity { get; set; }
    public List<Material>? Materials;
}


public class SendViewModel
{

    public List<MaterialInventory>? MaterialInventories { get; set; }

    [Required]
    public List<SendMaterialForm> SendMaterialForms { get; set; } = new List<SendMaterialForm> { new SendMaterialForm() };
}

public class SendMaterialForm
{

    [Required]
    public int MaterialInventoryId { get; set; }

    [Required]
    public int Quantity { get; set; }
}

