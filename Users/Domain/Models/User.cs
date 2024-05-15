using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace Users.Domain.Model;

public class User : IdentityUser<int>
{
    // Properties
    public string Name { get; set; }
    public string LastName { get; set; }
}