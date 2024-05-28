namespace Activities.Resources;

public class ActivityResource
{
    public int Id { get; set; }
    public string AppName { get; set; }
    public TimeSpan TotalTimeUsedPerDay { get; set; }
    public DateTime DateReported { get; set; }
    public int TotalNotifications { get; set; }
    public int DeviceId { get; set; }
}