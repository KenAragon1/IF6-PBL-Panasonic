using System.ComponentModel.DataAnnotations;
using panasonic.Models;

namespace panasonic.ViewModels.MaterialRequestViewModel;

public class CreateViewModel
{
    [Required]
    public int MaterialId { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required]
    public int ProductionLineId { get; set; }

    public List<ProductionLine>? ProductionLines { get; set; }
    public List<Material>? Materials { get; set; }
}