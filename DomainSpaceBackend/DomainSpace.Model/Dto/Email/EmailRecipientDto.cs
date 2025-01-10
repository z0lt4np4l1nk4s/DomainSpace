namespace DomainSpace.Model.Dto;

/// <summary>
/// Email recipient model
/// </summary>
public class EmailRecipientDto
{
    /// <summary>
    /// User identifier
    /// </summary>
    public Guid? UserId { get; set; }

    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Email
    /// </summary>
    public string Email { get; set; } = default!;

    /// <summary>
    /// Call to action link
    /// </summary>
    public string? CallToActionLink { get; set; } = default!;
}
