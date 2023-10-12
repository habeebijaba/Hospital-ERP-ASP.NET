using System;
using System.ComponentModel.DataAnnotations;

public class UniqueEmailAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        
        if (value is string email)
        {
            var userService = (IUserService)validationContext.GetService(typeof(IUserService));
            var existingUser = userService.GetUserByEmail(email);

            if (existingUser != null)
            {
                return new ValidationResult("Email is already in use.");
            }
        }

        return ValidationResult.Success;
    }
}
