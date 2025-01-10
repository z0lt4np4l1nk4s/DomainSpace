namespace DomainSpace.Model.Filter;

/// <summary>
/// Subject filter
/// </summary>
public class SubjectFilterDto : BaseParams
{
    /// <summary>
    /// Domain
    /// </summary>
    public string? Domain { get; set; }

    /// <summary>
    /// Name
    /// </summary>
    public string? Name { get; set; }
}
