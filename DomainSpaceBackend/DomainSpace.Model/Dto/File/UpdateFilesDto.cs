namespace DomainSpace.Model.Dto;

/// <summary>
/// Update files model
/// </summary>
public class UpdateFilesDto
{
    /// <summary>
    /// Owner identifier
    /// </summary>
    public Guid OwnerId { get; set; }

    /// <summary>
    /// Owner type
    /// </summary>
    public string OwnerType { get; set; } = default!;

    /// <summary>
    /// Domain
    /// </summary>
    public string Domain { get; set; } = default!;

    /// <summary>
    /// Old files
    /// </summary>
    public List<FileDto> OldFiles { get; set; } = default!;

    /// <summary>
    /// New files
    /// </summary>
    public List<AddFileDto> NewFiles { get; set; } = default!;
}
