namespace Shared.RequestHelpers;

public class ServiceParams : PaginationParams
{
    public string? OrderBy { get; set; }
    public string? SearchTerm { get; set; }
}