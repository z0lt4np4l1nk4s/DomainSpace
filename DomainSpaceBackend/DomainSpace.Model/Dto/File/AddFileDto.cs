namespace DomainSpace.Model.Dto;

/// <summary>
/// Add file model
/// </summary>
public class AddFileDto
{
    /// <summary>
    /// User identifier
    /// </summary>
    public Guid UserId { get; set; }

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

    /// <summary>
    /// Content
    /// </summary>
    public byte[] Content { get; set; } = default!;
}
