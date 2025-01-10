namespace DomainSpace.Model.Filter;

/// <summary>
/// File filter
/// </summary>
public class FileFilterDto
{
    /// <summary>
    /// Owner identifier
    /// </summary>
    public Guid OwnerId { get; set; }

    /// <summary>
    /// Owner type
    /// </summary>
    public string OwnerType { get; set; } = default!;
}
