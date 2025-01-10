namespace DomainSpace.Model.Entity;

/// <summary>
/// User entity
/// </summary>
public class UserEntity : IdentityUser<Guid>
{
    /// <summary>
    /// First name
    /// </summary>
    public string FirstName { get; set; } = default!;

    /// <summary>
    /// Last name
    /// </summary>
    public string LastName { get; set; } = default!;

    /// <summary>
    /// Date of birth
    /// </summary>
    public DateTime DateOfBirth { get; set; } = default!;

    /// <summary>
    /// Creation time
    /// </summary>
    public DateTime CreationTime { get; set; }

    /// <summary>
    /// Content
    /// </summary>
    public virtual ICollection<ContentEntity> Content { get; set; } = default!;

    /// <summary>
    /// Content
    /// </summary>
    public virtual ICollection<SubjectEntity> Subjects { get; set; } = default!;

    /// <summary>
    /// Files
    /// </summary>
    public virtual ICollection<FileEntity> Files { get; set; } = default!;

    /// <summary>
    /// Likes
    /// </summary>
    public virtual ICollection<LikeEntity> Likes { get; set; } = default!;

    /// <summary>
    /// Emails
    /// </summary>
    public virtual ICollection<EmailEntity> Emails { get; set; } = default!;
}
