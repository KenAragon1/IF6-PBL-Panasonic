using System.ComponentModel.DataAnnotations;

namespace panasonic.Validations;

public class UniqueEmployeeIdAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is int employeeId)
        {
            var userIdProperty = validationContext.ObjectType.GetProperty("Id");
            var context = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));

            var user = context.Users.FirstOrDefault(u => u.EmployeeID == employeeId);

            if (user != null)
            {
                if (userIdProperty == null) return new ValidationResult("This Employee Id is already taken");

                var userId = userIdProperty.GetValue(validationContext.ObjectInstance, null) as int?;

                if (user.Id != userId) return new ValidationResult("This Employee ID is already taken By Other User");
            }

        }
        else
        {
            return new ValidationResult($"The field {validationContext.DisplayName} must be an integer.");
        }
        return ValidationResult.Success;
    }
}