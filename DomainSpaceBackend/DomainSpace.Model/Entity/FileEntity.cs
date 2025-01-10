namespace DomainSpace.Model.Entity;

/// <summary>
/// File entity
/// </summary>
public class FileEntity : BaseEntity
{
    /// <summary>
    /// User identifier
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// User
    /// </summary>
    public UserEntity? User { get; set; }

    /// <summary>
    /// Relative path
    /// </summary>
    public string Path { get; set; } = default!;

    /// <summary>
    /// Local path
    /// </summary>
    public string LocalPath { get; set; } = default!;

    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Extension
    /// </summary>
    public string Extension { get; set; } = default!;

    /// <summary>
    /// Owner type
    /// </summary>
    public string OwnerType { get; set; } = default!;

    /// <summary>
    /// Owner identifier
    /// </summary>
    public Guid OwnerId { get; set; }

    /// <summary>
    /// Size
    /// </summary>
    public long Size { get; set; }
}
