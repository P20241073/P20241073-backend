using System;
using Users.Domain.Model;

namespace Activities.Domain.Model;

public class Activity 
{
    // Properties
    public int Id { get; set; }
    public string AppName { get; set; }
    public TimeSpan TotalTimeUsedPerDay { get; set; }
    public DateTime DateReported { get; set; }
    public int TotalNotifications { get; set; }
    public Device Device {get; set;}
    public int DeviceId { get; set; }
}