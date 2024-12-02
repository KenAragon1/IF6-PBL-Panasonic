using System.ComponentModel.DataAnnotations;
using panasonic.Models;

namespace panasonic.ViewModels.MaterialRequestViewModel;

public class CreateViewModel
{
    public List<CreateForm> CreateForms { get; set; } = new List<CreateForm> { new CreateForm() };

    public List<ProductionLine>? ProductionLines { get; set; }
    public List<Material>? Materials { get; set; }
}

public class CreateForm
{
    [Required]
    public int MaterialId { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required]
    public int ProductionLineId { get; set; }
}