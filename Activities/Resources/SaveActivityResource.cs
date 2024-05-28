using System.ComponentModel.DataAnnotations;

namespace Users.Resources;

public class SaveActivityResource
{
    [Required]
    [MaxLength(200)]
    public string AppName { get; set; }

    [Required]
    public TimeSpan TotalTimeUsedPerDay { get; set; }

    [Required]
    public DateTime DateReported { get; set; }

    [Required]
    public int TotalNotifications { get; set; }

    [Required]
    public int DeviceId { get; set; }
}