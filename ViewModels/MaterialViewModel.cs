using System.ComponentModel.DataAnnotations;
using panasonic.Validations;

namespace panasonic.ViewModels.MaterialViewModel;

public class MaterialViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Material Name Required")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Material Description Required")]
    public string Description { get; set; } = string.Empty;
}

public class CreateMaterialViewModel : MaterialViewModel
{
    [Required(ErrorMessage = "QR Code Image Required")]
    [ImageFile]
    public IFormFile QrCodeImage { get; set; }
}
public class EditMaterialViewModel : MaterialViewModel
{
    [ImageFile]
    public IFormFile? QrCodeImage { get; set; }

    public string OldQrCodeImageurl { get; set; } = string.Empty;
}

