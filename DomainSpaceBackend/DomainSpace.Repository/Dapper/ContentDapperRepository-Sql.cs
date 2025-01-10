namespace DomainSpace.Repository.Dapper;

/// <inheritdoc cref="IContentDapperRepository" />
public partial class ContentDapperRepository
{
    /// <summary>
    /// Get content paged
    /// </summary>
    /// <returns>Sql script</returns>
    public static string GetPagedAsyncSqlCommand() => ReadSqlData(nameof(GetPagedAsync));

    /// <summary>
    /// Count content
    /// </summary>
    /// <returns>Sql script</returns>
    public static string CountAsyncSqlCommand() => ReadSqlData(nameof(CountAsync));

    /// <summary>
    /// Get content by identifier
    /// </summary>
    /// <returns>Sql script</returns>
    public static string GetByIdAsyncSqlCommand() => ReadSqlData(nameof(GetByIdAsync));

    /// <summary>
    /// Reads sql script from the specified file
    /// </summary>
    /// <param name="fileName">File name</param>
    /// <returns>Sql script</returns>
    private static string ReadSqlData(string fileName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = $"{nameof(DomainSpace)}.{nameof(Repository)}.{nameof(Dapper)}.Sql.Content.{fileName}.sql";

        using var stream = assembly.GetManifestResourceStream(resourceName);

        if (stream == null)
        {
            throw new FileNotFoundException($"Embedded resource '{resourceName}' not found.");
        }

        using var reader = new StreamReader(stream, Encoding.UTF8);
        return reader.ReadToEnd();
    }
}
