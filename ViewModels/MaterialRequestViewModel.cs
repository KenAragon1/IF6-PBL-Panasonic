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
    public int DestinationId { get; set; }

    public List<Area>? Destinations { get; set; }
    public List<Material>? Materials { get; set; }
}