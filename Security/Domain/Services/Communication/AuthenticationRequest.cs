using System.ComponentModel.DataAnnotations;

namespace Security.Domain.Services.Communication;

public class AuthenticateRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}