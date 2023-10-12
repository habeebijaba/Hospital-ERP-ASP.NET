using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

public class User
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    [UniqueEmail]
    [RegularExpression(@"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$", ErrorMessage = "Invalid email format.")]
    public string Email { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
    public string Password { get; set; }

    [ValidateNever]
    public string Image { get; set; } // Path to the user's image file

    [Required]
    public DateTime DateOfBirth { get; set; }

    [Required]
    public int Age { get; set; }

    [Required]
    public string Place { get; set; }
}
