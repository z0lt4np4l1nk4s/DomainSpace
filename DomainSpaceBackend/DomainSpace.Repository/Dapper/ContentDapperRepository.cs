namespace DomainSpace.Repository.Dapper;

/// <inheritdoc cref="IContentDapperRepository" />
public partial class ContentDapperRepository : IContentDapperRepository
{
    private readonly IDbConnection _connection;
    private readonly AppOptions _appOptions;

    public ContentDapperRepository(IDbConnection connection, IOptions<AppOptions> appOptionsAccessor)
    {
        _connection = connection;
        _appOptions = appOptionsAccessor.Value;
    }

    /// <inheritdoc cref="IContentDapperRepository.CountAsync(ContentFilterDto, CancellationToken)" />
    public async Task<int> CountAsync(ContentFilterDto param, CancellationToken cancellationToken = default)
    {
        (var sql, var parameters) = BuildPagedListQuery(param, CountAsyncSqlCommand());

        var result = await _connection.ExecuteScalarAsync<int>(sql, parameters);

        return result;
    }

    /// <inheritdoc cref="IContentDapperRepository.GetByIdAsync(Guid, Guid, CancellationToken)" />
    public async Task<ContentDto?> GetByIdAsync(Guid id, Guid observerId, CancellationToken cancellationToken = default)
    {
        var parameters = new DynamicParameters();

        parameters.Add("@id", id);
        parameters.Add("@observerId", observerId);
        parameters.Add("@ownerType", nameof(ContentEntity));

        var splitOnIds = string.Join(", ", new[]
        {
            nameof(ContentDto.Id),
            nameof(ContentUserInfoDto.UserId),
            nameof(FileDto.FileId)
        });

        var types = new[]
        {
            typeof(ContentDto),
            typeof(ContentUserInfoDto),
            typeof(FileDto)
        };

        ContentDto? content = null;

        await _connection.QueryAsync(GetByIdAsyncSqlCommand(), types,
            results =>
            {
                if (content == null)
                {
                    content = (ContentDto)results[0];
                    content.User = (ContentUserInfoDto)results[1];
                }

                var file = (FileDto)results[2];

                if (file != null)
                {
                    content.Files ??= new List<FileDto>();
                    file.Path = $"{_appOptions.FilesPrefixUrl}/{file.Path}";
                    content.Files.Add(file);
                }

                return content;
            }, parameters, splitOn: splitOnIds);

        return content;
    }

    /// <inheritdoc cref="IContentDapperRepository.GetPagedAsync(ContentFilterDto, CancellationToken)" />
    public async Task<PagedList<ContentDto>> GetPagedAsync(ContentFilterDto param, CancellationToken cancellationToken = default)
    {
        (var sql, var parameters) = BuildPagedListQuery(param, GetPagedAsyncSqlCommand());

        parameters.Add("@observerId", param.ObserverId);
        parameters.Add("@pageSize", param.PageSize);
        parameters.Add("@offset", (param.PageNumber - 1) * param.PageSize);

        var splitOnIds = string.Join(", ", new[]
        {
            nameof(ContentDto.Id),
            nameof(ContentUserInfoDto.UserId),
            nameof(FileDto.FileId)
        });

        var types = new[]
        {
            typeof(ContentDto),
            typeof(ContentUserInfoDto),
            typeof(FileDto)
        };

        var contentDictionary = new Dictionary<Guid, ContentDto>();

        await _connection.QueryAsync(sql, types,
            results =>
            {
                var content = (ContentDto)results[0];
                var user = (ContentUserInfoDto)results[1];
                var file = (FileDto)results[2];

                if (!contentDictionary.TryGetValue(content.Id, out var contentEntry))
                {
                    contentEntry = content;
                    contentEntry.User = user;
                    contentDictionary.Add(contentEntry.Id, contentEntry);
                }

                if (file != null)
                {
                    contentEntry.Files ??= new List<FileDto>();
                    file.Path = $"{_appOptions.FilesPrefixUrl}/{file.Path}";
                    contentEntry.Files.Add(file);
                }

                return contentEntry;
            }, parameters, splitOn: splitOnIds);

        var resultList = contentDictionary.Values.ToList();

        var totalCount = await CountAsync(param, cancellationToken);

        var pagedList = new PagedList<ContentDto>
        {
            Items = resultList,
            PageNumber = param.PageNumber,
            PageSize = param.PageSize,
            TotalCount = totalCount
        };

        return pagedList;
    }

    private static (string Sql, DynamicParameters Parameters) BuildPagedListQuery(ContentFilterDto param, string sql)
    {
        var parameters = new DynamicParameters();
        var sqlBuilder = new SqlBuilder();

        parameters.Add("@ownerType", nameof(ContentEntity));
        sqlBuilder.Where("c.\"DeleteTime\" is null");

        if (!param.Query.IsNullOrEmpty())
        {
            parameters.Add("@query", $"{param.Query}%");
            sqlBuilder.Where("(c.\"Title\" ilike @query)");
        }

        if (!param.SubjectIds.IsNullOrEmpty())
        {
            var subjectIds = param.SubjectIds!
                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                .Select(x => Guid.Parse(x));

            parameters.Add("@subjectIds", subjectIds.ToArray());
            sqlBuilder.Where("c.\"SubjectId\" = ANY(@subjectIds)");
        }

        if (!param.Domain.IsNullOrEmpty())
        {
            parameters.Add("@domain", param.Domain);
            sqlBuilder.Where("c.\"Domain\" = @domain");
        }

        string? orderBy = null;

        if (!param.OrderBy.IsNullOrEmpty())
        {
            var ordering = OrderingUtil.GetOrdering<UserDto>(param.OrderBy!);
            orderBy = string.Join(", ", ordering.Select(x => "\"" + x.Property + "\" " + x.Direction));
        }

        if (string.IsNullOrEmpty(orderBy))
        {
            orderBy = "c.\"CreationTime\" DESC";
        }

        sqlBuilder.OrderBy(orderBy);

        var template = sqlBuilder.AddTemplate(sql);

        return (template.RawSql, parameters);
    }
}
