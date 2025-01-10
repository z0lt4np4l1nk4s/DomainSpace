namespace DomainSpace.Model.Filter;

/// <summary>
/// User filter
/// </summary>
public class UserFilterDto : BaseParams
{
    /// <summary>
    /// Roles
    /// </summary>
    public string? Roles { get; set; }
}
