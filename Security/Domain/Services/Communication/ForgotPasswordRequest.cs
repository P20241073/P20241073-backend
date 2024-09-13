using System.ComponentModel.DataAnnotations;

namespace Security.Domain.Services.Communication;

public class ForgotPasswordRequest
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    public string? ClientUri { get; set; }
}