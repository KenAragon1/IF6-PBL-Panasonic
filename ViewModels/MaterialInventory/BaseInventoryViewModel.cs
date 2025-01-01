using System.ComponentModel.DataAnnotations;
using panasonic.Models;

namespace panasonic.ViewModels.MaterialInventoryViewModels;

public class BaseInventoryViewModel
{

    [Required(ErrorMessage = "Production line can't be empty")]
    [Range(1, int.MaxValue, ErrorMessage = "Production line can't be empty")]

    public int ProductionLineId { get; set; }

    [Required(ErrorMessage = "Form cant be empty")]
    [MinLength(1, ErrorMessage = "Atleast one inventory is needed")]
    public List<InventoryFormViewModel> InventoryForms { get; set; } = new List<InventoryFormViewModel>();

    public List<MaterialInventory> Inventories { get; set; } = new List<MaterialInventory>();
    public List<Material> Materials { get; set; } = new List<Material>();
    public List<ProductionLine> ProductionLines { get; set; } = new List<ProductionLine>();

}