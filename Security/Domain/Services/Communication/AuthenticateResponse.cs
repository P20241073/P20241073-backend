using System.ComponentModel.DataAnnotations;

namespace Security.Domain.Services.Communication;

public class AuthenticateResponse
{
    public int Id {get; set;}
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
}