using System;
using Users.Domain.Model;

namespace Reports.Domain.Model;

public class SasSv 
{
    public int Id { get; set; }
    public int DeviceId { get; set; }
    public Device Device { get; set; }
    public int Item1 { get; set; }
    public int Item2 { get; set; }
    public int Item3 { get; set; }
    public int Item4 { get; set; }
    public int Item5 { get; set; }
    public int Item6 { get; set; }
    public int Item7 { get; set; }
    public int Item8 { get; set; }
    public int Item9 { get; set; }
    public int Item10 { get; set; }
    public DateTime DateTaken { get; set; }
}