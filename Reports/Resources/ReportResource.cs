namespace Reports.Resources;

public class ReportResource
{
    public int Id { get; set; }
    public int AverageTimeUsedPerDay { get; set; }
    public bool UsesSocialMedia { get; set; }
    public string MostUsedApp { get; set; }
    public DateTime DateTaken { get; set; }
}