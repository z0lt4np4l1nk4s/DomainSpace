namespace DomainSpace.Common.Extensions;

/// <summary>
/// String extensions
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Indicates whether the string is null or ""
    /// </summary>
    /// <param name="value">String value</param>
    /// <returns>True if empty or null otherwise false</returns>
    public static bool IsNullOrEmpty(this string? value)
    {
        return string.IsNullOrEmpty(value);
    }

    /// <summary>
    /// Split string by comma
    /// </summary>
    /// <typeparam name="T">List type</typeparam>
    /// <param name="value">String value</param>
    /// <returns>List of items</returns>
    public static T[] Split<T>(this string value)
    {
        var list = value.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

        var result = new List<T>();

        foreach (var item in list)
        {
            T convertedItem = (T)Convert.ChangeType(item, typeof(T));
            result.Add(convertedItem);
        }

        return result.ToArray();
    }

    /// <summary>
    /// Convert string to title case
    /// </summary>
    /// <param name="value">String value</param>
    /// <returns>The converted string</returns>
    public static string ToTitleCase(this string value)
    {
        return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(value.ToLower());
    }
}
