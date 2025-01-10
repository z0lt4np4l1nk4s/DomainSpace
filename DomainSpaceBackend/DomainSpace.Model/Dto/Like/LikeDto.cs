namespace DomainSpace.Model.Dto;

/// <summary>
/// Like model
/// </summary>
public class LikeDto
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
