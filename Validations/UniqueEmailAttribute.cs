using System.ComponentModel.DataAnnotations;

namespace panasonic.Validations;

public class UniqueEmailAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var email = value as string;
        var userIdProperty = validationContext.ObjectType.GetProperty("Id");
        var context = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));

        var user = context.Users.FirstOrDefault(u => u.Email == email);

        if (user != null)
        {
            if (userIdProperty == null) return new ValidationResult("This Email is already taken");

            var userId = userIdProperty.GetValue(validationContext.ObjectInstance, null) as int?;

            if (user.Id != userId) return new ValidationResult("This Email is already taken By Other User");
        }


        return ValidationResult.Success;
    }


}
