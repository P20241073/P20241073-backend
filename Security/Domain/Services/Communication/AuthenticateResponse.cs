using System.ComponentModel.DataAnnotations;

namespace Security.Domain.Services.Communication;

public class AuthenticateResponse
{
    public string Email { get; set; }
    public string Token { get; set; }
}