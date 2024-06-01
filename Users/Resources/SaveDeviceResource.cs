using System.ComponentModel.DataAnnotations;

namespace Users.Resources;

public class SaveDeviceResource
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; }

    [Range(1, 120, ErrorMessage = "Age must be a positive number.")]
    public int Age { get; set; }

    [Range(1, 5, ErrorMessage = "Age must be a positive number.")]
    public int Grade { get; set; }

    [Required]
    [MaxLength(600)]
    public string Info { get; set; }
    
    [Required]
    public string UserType { get; set; }
    
    [Required]
    public int UserId { get; set; }
}