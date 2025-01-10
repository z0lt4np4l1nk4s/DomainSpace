namespace DomainSpace.Common.Extensions;

/// <summary>
/// Queryable extensions
/// </summary>
public static class QueryableExtensions
{
    /// <summary>
    /// Order data by the given property and direction
    /// </summary>
    /// <typeparam name="T">Data type</typeparam>
    /// <param name="source">Queryable source</param>
    /// <param name="propertyName">Property name</param>
    /// <param name="direction">Order direction</param>
    /// <returns>Queryable</returns>
    public static IQueryable<T> OrderByProperty<T>(this IQueryable<T> source, string propertyName, string direction = "asc")
    {
        var param = Expression.Parameter(typeof(T), "x");
        var property = Expression.Property(param, propertyName);
        var selector = Expression.Lambda(property, param);

        var method = direction.ToLower() == "asc" ? "OrderBy" : "OrderByDescending";
        var genericMethod = typeof(Queryable).GetMethods().Single(
            m => m.Name == method && m.IsGenericMethodDefinition && m.GetGenericArguments().Length == 2 && m.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(T), property.Type);

        var queryExpr = Expression.Call(genericMethod, source.Expression, selector);
        return source.Provider.CreateQuery<T>(queryExpr);
    }

    /// <summary>
    /// Order data by the given property and direction
    /// </summary>
    /// <typeparam name="T">Data type</typeparam>
    /// <param name="source">Queryable source</param>
    /// <param name="ordering"></param>
    /// <returns>Queryable</returns>
    public static IQueryable<T> OrderByProperty<T>(this IQueryable<T> source, OrderingDto ordering)
    {
        return OrderByProperty<T>(source, ordering.Property, ordering.Direction);
    }
}
