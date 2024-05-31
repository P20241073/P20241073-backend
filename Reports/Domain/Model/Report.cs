using System;
using Users.Domain.Model;

namespace Reports.Domain.Model;

public class Report 
{
    public int Id { get; set; }
    public int DeviceId { get; set; }
    public Device Device { get; set; }
    public int AverageTimeUsedPerDay { get; set; }
    public bool UsesSocialMedia { get; set; }
    public string MostUsedApp { get; set; }
    public DateTime DateTaken { get; set; }
}
