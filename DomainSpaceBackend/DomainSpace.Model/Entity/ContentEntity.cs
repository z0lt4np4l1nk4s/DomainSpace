namespace DomainSpace.Model.Entity;

/// <summary>
/// Content entity
/// </summary>
public class ContentEntity : BaseEntity
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
    /// Subject identifier
    /// </summary>
    public Guid SubjectId { get; set; }

    /// <summary>
    /// Subject
    /// </summary>
    public SubjectEntity? Subject { get; set; } 

    /// <summary>
    /// Domain
    /// </summary>
    public string Domain { get; set; } = default!;

    /// <summary>
    /// Title
    /// </summary>
    public string Title { get; set; } = default!;

    /// <summary>
    /// Text
    /// </summary>
    public string Text { get; set; } = default!;

    /// <summary>
    /// Likes count
    /// </summary>
    public int LikeCount { get; set; }

    /// <summary>
    /// Likes
    /// </summary>
    public virtual ICollection<LikeEntity> Likes { get; set; } = default!;
}
