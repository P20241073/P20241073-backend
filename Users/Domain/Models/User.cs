using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace Users.Domain.Model;

public class User : IdentityUser
{
    // Properties
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

}