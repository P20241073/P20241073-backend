using System.ComponentModel.DataAnnotations;

namespace Reports.Resources;

public class SavReportResource
{
    [Required]
    public int DeviceId { get; set; }
    
    [Required]
    public int AverageTimeUsedPerDay { get; set; }
    
    [Required]
    public bool UsesSocialMedia { get; set; }
    
    [Required]
    public string MostUsedApp { get; set; }
    
    [Required]
    public DateTime DateTaken { get; set; }
}