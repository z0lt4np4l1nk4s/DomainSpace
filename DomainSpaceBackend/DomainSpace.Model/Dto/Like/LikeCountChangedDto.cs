namespace DomainSpace.Model.Dto;

/// <summary>
/// Like count changed model
/// </summary>
public class LikeCountChangedDto
{
    /// <summary>
    /// Content identifier
    /// </summary>
    public Guid ContentId { get; set; }

    /// <summary>
    /// Count
    /// </summary>
    public int Count { get; set; }
}
