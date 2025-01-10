namespace DomainSpace.Model.Dto;

/// <summary>
/// Dislike model
/// </summary>
public class DislikeDto
{
    /// <summary>
    /// User identifier
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Content identifier
    /// </summary>
    public Guid ContentId { get; set; }
}
