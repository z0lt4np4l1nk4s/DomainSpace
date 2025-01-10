namespace DomainSpace.Repository.Dapper;

/// <inheritdoc cref="IUserDapperRepository" />
public partial class UserDapperRepository : IUserDapperRepository
{
    private readonly IDbConnection _connection;

    public UserDapperRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    /// <inheritdoc cref="IUserDapperRepository.CountAsync(UserFilterDto, CancellationToken)" />
    public async Task<int> CountAsync(UserFilterDto param, CancellationToken cancellationToken = default)
    {
        (var sql, var parameters) = BuildPagedListQuery(param, CountAsyncSqlCommand());

        var result = await _connection.ExecuteScalarAsync<int>(sql, parameters);

        return result;
    }

    /// <inheritdoc cref="IUserDapperRepository.GetPagedAsync(UserFilterDto, CancellationToken)" />
    public async Task<PagedList<UserDto>> GetPagedAsync(UserFilterDto param, CancellationToken cancellationToken = default)
    {
        (var sql, var parameters) = BuildPagedListQuery(param, GetPagedAsyncSqlCommand());

        parameters.Add("@pageSize", param.PageSize);
        parameters.Add("@offset", (param.PageNumber - 1) * param.PageSize);

        var splitOnIds = string.Join(", ", new[]
        {
            nameof(UserDto.Id),
            "RoleName"
        });

        var types = new[]
        {
            typeof(UserDto),
            typeof(string)
        };

        var usersDictionary = new Dictionary<Guid, UserDto>();

        await _connection.QueryAsync(sql, types,
            results =>
            {
                var user = (UserDto)results[0];
                var roleName = (string)results[1];

                if(!usersDictionary.TryGetValue(user.Id, out var userEntry))
                {
                    userEntry = user;
                    userEntry.Roles = new List<string>();
                    usersDictionary.Add(userEntry.Id, userEntry);
                }

                userEntry.Roles!.Add(roleName);
                return userEntry;
            }, parameters, splitOn: splitOnIds);

        var resultList = usersDictionary.Values.ToList();

        var totalCount = await CountAsync(param, cancellationToken);

        var pagedList = new PagedList<UserDto>
        {
            Items = resultList,
            PageNumber = param.PageNumber,
            PageSize = param.PageSize,
            TotalCount = totalCount
        };

        return pagedList;
    }

    private static (string Sql, DynamicParameters Parameters) BuildPagedListQuery(UserFilterDto param, string sql)
    {
        var parameters = new DynamicParameters();
        var sqlBuilder = new SqlBuilder();

        if (!param.Query.IsNullOrEmpty())
        {
            parameters.Add("@query", $"{param.Query}%");
            sqlBuilder.Where("(u.\"Email\" ilike @query or u.\"FirstName\" ilike @query or u.\"LastName\" ilike @query)");
        }

        if (!param.Roles.IsNullOrEmpty())
        {
            parameters.Add("@roles", param.Roles!.Split<string>().Select(x => x.ToLower()).ToArray());
            sqlBuilder.Where("LOWER(r.\"Name\") = ANY(@roles)");
        }

        string? orderBy = null;

        if (!param.OrderBy.IsNullOrEmpty())
        {
            var ordering = OrderingUtil.GetOrdering<UserDto>(param.OrderBy!);
            orderBy = string.Join(", ", ordering.Select(x => "\"" + x.Property + "\" " + x.Direction));
        }

        if (string.IsNullOrEmpty(orderBy))
        {
            orderBy = "u.\"CreationTime\" DESC";
        }

        sqlBuilder.OrderBy(orderBy);

        var template = sqlBuilder.AddTemplate(sql);

        return (template.RawSql, parameters);
    }
}
