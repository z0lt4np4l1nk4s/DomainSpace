namespace DomainSpace.Model.Dto;

/// <summary>
/// Content model
/// </summary>
public class ContentDto
{
    /// <summary>
    /// Identifier
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// User
    /// </summary>
    public ContentUserInfoDto? User { get; set; }
    
    /// <summary>
    /// Subject identifier
    /// </summary>
    public Guid SubjectId { get; set; }

    /// <summary>
    /// Subject name
    /// </summary>
    public string SubjectName { get; set; } = default!;

    /// <summary>
    /// Domain
    /// </summary>
    public string Domain { get; set; } = default!;

    /// <summary>
    /// Title
    /// </summary>
    public string Title { get; set; } = default!;

    /// <summary>
    /// Text
    /// </summary>
    public string Text { get; set; } = default!;

    /// <summary>
    /// Creation time
    /// </summary>
    public DateTime CreationTime { get; set; }

    /// <summary>
    /// Files
    /// </summary>
    public List<FileDto>? Files { get; set; }

    /// <summary>
    /// Like count
    /// </summary>
    public int LikeCount { get; set; }

    /// <summary>
    /// Liked
    /// </summary>
    public bool Liked { get; set; }
}
