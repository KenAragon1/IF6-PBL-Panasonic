using System.ComponentModel.DataAnnotations;
using panasonic.Models;

namespace panasonic.ViewModels.ProductionLineViewModel;

public class IndexViewModel
{
    public List<Area> areas;
}

public class ManageViewModel
{
    public List<Material> materials;

    public List<User> users;
}

public class CreateViewModel
{
    [Required]
    public int LineNumber { get; set; }
}