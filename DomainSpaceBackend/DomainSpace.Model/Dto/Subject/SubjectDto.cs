namespace DomainSpace.Model.Dto;

/// <summary>
/// Subject model
/// </summary>
public class SubjectDto
{
    /// <summary>
    /// Identifier
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// User identifier
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Domain
    /// </summary>
    public string Domain { get; set; } = default!;

    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; } = default!;
}
