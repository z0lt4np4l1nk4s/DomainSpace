namespace DomainSpace.Common.Dto;

/// <summary>
/// Paging params
/// </summary>
public class PagingParams
{
    /// <summary>
    /// Page number
    /// </summary>
    public int PageNumber { get; set; } = 1;

    /// <summary>
    /// Page size
    /// </summary>
    public int PageSize { get; set; } = 10;
}
