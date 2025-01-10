namespace DomainSpace.Model.Dto;

/// <summary>
/// Add content model
/// </summary>
public class AddContentDto
{
    /// <summary>
    /// User identifier
    /// </summary>
    public Guid? UserId { get; set; }

    /// <summary>
    /// Subject identifier
    /// </summary>
    public Guid? SubjectId { get; set; }

    /// <summary>
    /// Domain
    /// </summary>
    public string? Domain { get; set; }

    /// <summary>
    /// Title
    /// </summary>
    public string Title { get; set; } = default!;

    /// <summary>
    /// Text
    /// </summary>
    public string? Text { get; set; }
}
