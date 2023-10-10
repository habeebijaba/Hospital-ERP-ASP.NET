
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

public class Doctor
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Department { get; set; }
    
    [Required]
    public int Charge { get; set; }
    
    [Required]
    public string Availability { get; set; }
    
    // [Required]
    [ValidateNever]
    public string ImagePath { get; set; }
}

