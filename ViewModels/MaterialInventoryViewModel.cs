using System.ComponentModel.DataAnnotations;
using panasonic.Models;

namespace panasonic.ViewModels.MaterialInventoryViewModel;


public class MaterialTableData
{
    public required List<MaterialInventory> MaterialInventories { get; set; }
}


public class PreperationRoomViewModel : MaterialTableData
{

}


public class SendViewModel
{
    public List<MaterialRequest> MaterialRequests { get; set; } = [];

    public List<SendForm> Forms { get; set; } = [];
}

public class SendForm
{
    [Required]
    public int MaterialRequestId { get; set; }

    [Required]
    public int QuantitySend { get; set; }

    [Required]
    public string Measurement { get; set; } = string.Empty;
}


public class PickupViewModel
{
    [Required]
    public int ProductionLineDestination { get; set; }

    [Required]
    public List<MaterialInventoryForm> Forms { get; set; } = new List<MaterialInventoryForm> { new MaterialInventoryForm() };

    public List<ProductionLine>? ProductionLineOptions { get; set; }
    public List<Material> Materials { get; set; } = new List<Material>();
}

public class ReturnViewModel
{
    [Required]
    public int ProductionLineId { get; set; }

    public List<MaterialInventoryForm> Forms { get; set; } = new List<MaterialInventoryForm> { new MaterialInventoryForm() };

    public List<MaterialInventory> MaterialInventories { get; set; } = new List<MaterialInventory>();
    public List<ProductionLine> ProductionLines { get; set; } = new List<ProductionLine>();
}

public class UseViewModel
{
    public int ProductionLineId { get; set; }

    public List<MaterialInventoryForm> Forms { get; set; } = new List<MaterialInventoryForm> { new MaterialInventoryForm() };

    public List<MaterialInventory> MaterialInventories { get; set; } = new List<MaterialInventory>();
    public List<ProductionLine> ProductionLines { get; set; } = new List<ProductionLine>();
}

public class MaterialInventoryForm
{
    [Required(ErrorMessage = "Please pick a material")]
    public int? MaterialInventoryId { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
    public int Quantity { get; set; }
}


