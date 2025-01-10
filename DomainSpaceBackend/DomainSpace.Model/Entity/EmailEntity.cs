namespace DomainSpace.Model.Entity;

/// <summary>
/// Email entity
/// </summary>
public class EmailEntity : BaseEntity
{
    /// <summary>
    /// User identifier
    /// </summary>
    public Guid? UserId { get; set; }

    /// <summary>
    /// User
    /// </summary>
    public UserEntity? User { get; set; }

    /// <summary>
    /// Email
    /// </summary>
    public string? Email { get; set; } = default!;

    /// <summary>
    /// Email template
    /// </summary>
    public string Template { get; set; } = default!;

    /// <summary>
    /// Title
    /// </summary>
    public string Title { get; set; } = default!;

    /// <summary>
    /// Data
    /// </summary>
    public string Data { get; set; } = default!;
}
