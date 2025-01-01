using System.ComponentModel.DataAnnotations;

namespace panasonic.ViewModels.MaterialViewModels;

public class BaseMaterialViewModel
{
    [Required(ErrorMessage = "Material name can't be empty")]
    public string MaterialName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Material number can't be empty")]
    [Range(1, int.MaxValue, ErrorMessage = "Material number must atleast be 1")]
    public int MaterialNumber { get; set; }

    [Required(ErrorMessage = "Barcode can't be empty")]
    public string Barcode { get; set; } = string.Empty;

    [Required(ErrorMessage = "Unit measurement can't be empty")]
    public string UnitMeasurement { get; set; } = string.Empty;

    [Required(ErrorMessage = "Detail measurement can't be empty")]
    public string DetailMeasurement { get; set; } = string.Empty;

    [Required(ErrorMessage = "Detail quantity can't be empty")]
    [Range(1, int.MaxValue, ErrorMessage = "Detail quantity must atleast be 1")]
    public int DetailQuantity { get; set; }

}