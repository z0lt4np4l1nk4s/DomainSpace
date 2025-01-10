namespace DomainSpace.Model.Entity;

/// <summary>
/// Like entity
/// </summary>
public class LikeEntity : BaseEntity
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
    /// Content identifier
    /// </summary>
    public Guid ContentId { get; set; }

    /// <summary>
    /// Content
    /// </summary>
    public ContentEntity? Content { get; set; }
}