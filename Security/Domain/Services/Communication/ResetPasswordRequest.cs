using System.ComponentModel.DataAnnotations;

namespace Security.Domain.Services.Communication;

public class ResetPasswordRequest
{
    public string? Email { get; set; }
    public string? Token { get; set; }
    
    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }
    
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public string? ConfirmPassword { get; set; }
}