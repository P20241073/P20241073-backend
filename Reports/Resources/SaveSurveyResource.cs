using System.ComponentModel.DataAnnotations;

namespace Reports.Resources;

public class SaveSurveyResource
{
    [Required]
    public int DeviceId { get; set; }
    
    [Required]
    public int Item1 { get; set; }
    
    [Required]
    public int Item2 { get; set; }
    
    [Required]
    public int Item3 { get; set; }
    
    [Required]
    public int Item4 { get; set; }
    
    [Required]    
    public int Item5 { get; set; }

    [Required]
    public int Item6 { get; set; }

    [Required]
    public int Item7 { get; set; }

    [Required]
    public int Item8 { get; set; }

    [Required]
    public int Item9 { get; set; }

    [Required]
    public int Item10 { get; set; }

    [Required]
    public int TotalSum {get; set;}

    [Required]
    public DateTime DateTaken { get; set; }
}