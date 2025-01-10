namespace DomainSpace.Repository.Dapper;

/// <inheritdoc cref="IUserDapperRepository" />
public partial class UserDapperRepository
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
    /// Reads sql script from the specified file
    /// </summary>
    /// <param name="fileName">File name</param>
    /// <returns>Sql script</returns>
    private static string ReadSqlData(string fileName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = $"{nameof(DomainSpace)}.{nameof(Repository)}.{nameof(Dapper)}.Sql.User.{fileName}.sql";

        using var stream = assembly.GetManifestResourceStream(resourceName);

        if (stream == null)
        {
            throw new FileNotFoundException($"Embedded resource '{resourceName}' not found.");
        }

        using var reader = new StreamReader(stream, Encoding.UTF8);
        return reader.ReadToEnd();
    }
}
