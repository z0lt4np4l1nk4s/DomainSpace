namespace DomainSpace.Model.Entity;

/// <summary>
/// Subject entity
/// </summary>
public class SubjectEntity : BaseEntity
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
    /// Domain
    /// </summary>
    public string Domain { get; set; } = default!;

    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Content
    /// </summary>
    public virtual ICollection<ContentEntity> Content { get; set; } = default!;
}
