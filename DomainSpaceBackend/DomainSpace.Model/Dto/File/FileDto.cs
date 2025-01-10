namespace DomainSpace.Model.Dto;

/// <summary>
/// File model
/// </summary>
public class FileDto
{
    /// <summary>
    /// File identifier
    /// </summary>
    public Guid FileId { get; set; }

    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Extension
    /// </summary>
    public string Extension { get; set; } = default!;

    /// <summary>
    /// Size
    /// </summary>
    public long Size { get; set; }

    /// <summary>
    /// Relative path
    /// </summary>
    public string Path { get; set; } = default!;
}
