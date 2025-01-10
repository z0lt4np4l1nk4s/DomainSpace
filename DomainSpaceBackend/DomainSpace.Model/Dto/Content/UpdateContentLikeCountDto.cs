namespace DomainSpace.Model.Dto;

/// <summary>
/// Update content like count model
/// </summary>
public class UpdateContentLikeCountDto
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
