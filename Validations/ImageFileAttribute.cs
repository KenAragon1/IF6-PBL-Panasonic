using System.ComponentModel.DataAnnotations;

namespace panasonic.Validations;

public class ImageFileAttribute : ValidationAttribute
{
    private readonly string[] _validTypes = { "image/jpeg", "image/png", "image/jpg" };

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var file = value as IFormFile;

        if (file != null)
        {
            if (!_validTypes.Contains(file.ContentType))
            {
                return new ValidationResult("The file must be an image (JPG, JPEG, or PNG).");
            }
        }



        return ValidationResult.Success;
    }
}