using System.ComponentModel.DataAnnotations;

namespace Security.Domain.Services.Communication;

public class UpdateUserRequest
{
    [Required]
    public string Id { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string LastName { get; set; }
}