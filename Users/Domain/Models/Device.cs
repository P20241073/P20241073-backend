using System.Text.Json.Serialization;
using Activites.Domain.Model;

namespace Users.Domain.Model;

public class Device 
{
    // Properties
    public int Id { get; set; }
    public string Name { get; set; }
    public string Info { get; set; }
    public string UserType { get; set; }
    public int UserId { get; set; }
    public User User {get; set;}

    public IList<Activity> Activities {get; set;}
}