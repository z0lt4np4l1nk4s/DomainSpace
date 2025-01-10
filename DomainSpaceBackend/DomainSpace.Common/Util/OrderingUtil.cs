namespace DomainSpace.Common.Util;

/// <summary>
/// Ordering util
/// </summary>
public static class OrderingUtil
{
    private static readonly List<string> OrderingDirections = new()
    {
        "ASC", "DESC"
    };

    /// <summary>
    /// Splits the ordering in a list with directions
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    /// <param name="orderBy">Order by</param>
    /// <returns>List of ordering</returns>
    public static List<OrderingDto> GetOrdering<T>(string orderBy)
    {
        var ordering = new List<OrderingDto>();

        if (string.IsNullOrEmpty(orderBy))
        {
            return ordering;
        }

        foreach (string order in orderBy.Split(','))
        {
            var parts = order.Trim().Split('|');

            var propertyInfo = typeof(T).GetProperty(parts[0], BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (propertyInfo == null)
            {
                continue;
            }

            if (!OrderingDirections.Contains(parts[1].ToUpper()))
            {
                continue;
            }

            ordering.Add(new OrderingDto() { Property = propertyInfo.Name, Direction = parts[1].ToUpper() });
        }

        return ordering;
    }
}
