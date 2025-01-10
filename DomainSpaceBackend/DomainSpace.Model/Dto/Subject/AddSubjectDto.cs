namespace DomainSpace.Model.Dto;

/// <summary>
/// Add subject model
/// </summary>
public class AddSubjectDto
{
    /// <summary>
    /// User identifier
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Domain
    /// </summary>
    public string? Domain { get; set; }

    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; } = default!;
}
