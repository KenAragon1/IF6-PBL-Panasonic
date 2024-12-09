using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using panasonic.Models;

namespace panasonic.ViewModels.MaterialInventoryViewModel;


public class MaterialTableData
{
    public required List<MaterialInventory> MaterialInventories { get; set; }
}


public class PreperationRoomViewModel : MaterialTableData
{

}


public class PickupViewModel
{
    [Required]
    public int ProductionLineDestination { get; set; }

    [Required]
    public List<MaterialInventoryForm> Forms { get; set; } = new List<MaterialInventoryForm> { new MaterialInventoryForm() };

    public List<ProductionLine>? ProductionLineOptions { get; set; }
    public List<MaterialInventory>? MaterialInventoryOptions { get; set; }
}

public class MaterialInventoryForm
{
    [Required]
    public int MaterialInventoryId { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
    public int Quantity { get; set; }
}


