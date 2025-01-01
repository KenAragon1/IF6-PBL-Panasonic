using System.ComponentModel.DataAnnotations;

namespace panasonic.Dtos.MaterialDtos;

public class MaterialDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Material Name Required")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Material Number Required")]
    public int? Number { get; set; } = 0;

    [Required(ErrorMessage = "Material Unit Required")]
    public string UnitMeasurement { get; set; } = string.Empty;

    [Required(ErrorMessage = "Detail Measurement Required")]
    public string DetailMeasurement { get; set; } = string.Empty;

    [Required(ErrorMessage = "Detail Quantity Required")]
    public int? DetailQuantity { get; set; } = 0;
}