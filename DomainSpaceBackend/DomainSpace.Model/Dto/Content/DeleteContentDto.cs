namespace DomainSpace.Model.Dto;

/// <summary>
/// Delete content model
/// </summary>
public class DeleteContentDto
{
    /// <summary>
    /// Identifier
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// User identifier
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Is moderator or admin
    /// </summary>
    public bool IsModeratorOrAdmin { get; set; }
}
