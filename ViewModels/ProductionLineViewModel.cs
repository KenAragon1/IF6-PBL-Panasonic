using System.ComponentModel.DataAnnotations;
using panasonic.Models;

namespace panasonic.ViewModels.ProductionLineViewModel;

public class IndexViewModel
{
    public required List<ProductionLine> ProductionLines;
}

public class ManageViewModel
{
    public required List<Material> materials;

}

public class CreateViewModel
{
    [Required]
    public int Remark { get; set; }

    [Required]
    public string Description { get; set; } = string.Empty;
}

public class EditViewModel
{
    [Required]
    public int Remark { get; set; }

    [Required]
    public string Description { get; set; } = string.Empty;

    public ProductionLine ProductionLine { get; set; } = new ProductionLine();
}