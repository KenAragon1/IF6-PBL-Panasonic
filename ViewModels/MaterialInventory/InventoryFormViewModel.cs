using System.ComponentModel.DataAnnotations;

namespace panasonic.ViewModels.MaterialInventoryViewModels;

public class InventoryFormViewModel
{
    [Required(ErrorMessage = "Inventory id can't be empty")]
    public int InventoryId { get; set; }

    [Required(ErrorMessage = "Quantity can't be empty")]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be atleast 1")]
    public int Quantity { get; set; }
}