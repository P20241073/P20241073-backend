using System.ComponentModel.DataAnnotations;

namespace Users.Resources;

public class SaveDeviceResource
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; }

    [Required]
    [MaxLength(600)]
    public string Info { get; set; }
    
    [Required]
    public string UserType { get; set; }
    
    [Required]
    public int UserId { get; set; }
}