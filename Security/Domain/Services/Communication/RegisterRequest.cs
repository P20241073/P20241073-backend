namespace Security.Domain.Services.Communication;

public class RegisterRequest : AuthenticateRequest
{
    public string Name { get; set; }
    public string LastName { get; set; }
}