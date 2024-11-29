using System.ComponentModel.DataAnnotations;
using panasonic.Data.DTOs;
using panasonic.Models;

namespace panasonic.ViewModels.StoreViewModel;

public class IndexViewModel
{
    public required List<AreaMaterialDTO> materials;
}

public class CreateMaterialStockViewModel
{
    [Required(ErrorMessage = "Please select a material")]
    public int MaterialId { get; set; }

    [Required(ErrorMessage = "Quantity required")]
    [Range(1, 100, ErrorMessage = "Quantity must in range 1 - 100")]
    public int Quantity { get; set; }

    [Required(ErrorMessage = "Expiration date required")]
    public DateOnly ExpirationDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

    public List<Material>? Materials { get; set; }
}