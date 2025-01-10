namespace DomainSpace.Model.Dto;

/// <summary>
/// Update content model
/// </summary>
public class UpdateContentDto
{
    /// <summary>
    /// User identifier
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Is admin
    /// </summary>
    public bool IsAdmin { get; set; }

    /// <summary>
    /// Subject identifier
    /// </summary>
    public Guid SubjectId { get; set; }

    /// <summary>
    /// Title
    /// </summary>
    public string Title { get; set; } = default!;

    /// <summary>
    /// Text
    /// </summary>
    public string Text { get; set; } = default!;

    /// <summary>
    /// Files
    /// </summary>
    public List<FileDto> OldFiles { get; set; } = default!;
}
