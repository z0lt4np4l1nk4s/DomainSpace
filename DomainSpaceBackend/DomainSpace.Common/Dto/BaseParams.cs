namespace DomainSpace.Common.Dto;

/// <summary>
/// Base params model
/// </summary>
public class BaseParams : PagingParams
{
    /// <summary>
    /// Query
    /// </summary>
    public string? Query { get; set; }

    /// <summary>
    /// Order by
    /// </summary>
    public string? OrderBy { get; set; }
}
