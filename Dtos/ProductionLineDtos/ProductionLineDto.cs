using System.ComponentModel.DataAnnotations;

namespace panasonic.Dtos.ProductionLineDtos;

public class ProductionLineDto
{
    public int Id { get; set; }

    [Required]
    public int Remark { get; set; }

    [Required]
    public string Description { get; set; } = string.Empty;
}