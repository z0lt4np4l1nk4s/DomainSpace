namespace DomainSpace.Service;

/// <inheritdoc cref="ISubjectService" />
public class SubjectService : ISubjectService
{
    private const string DefaultSubjectName = "Default";

    private readonly IGenericRepository<SubjectEntity> _repository;
    private readonly IMapper _mapper;

    public SubjectService(IGenericRepository<SubjectEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <inheritdoc cref="ISubjectService.AddAsync(AddSubjectDto, CancellationToken)" />
    public async Task<ServiceResult> AddAsync(AddSubjectDto model, CancellationToken cancellationToken = default)
    {
        if (model.Name == DefaultSubjectName)
        {
            return ServiceResult.Failure();
        }

        var entity = _mapper.Map<SubjectEntity>(model);
        entity.Id = Guid.NewGuid();
        entity.CreationTime = entity.UpdateTime = DateTime.UtcNow;

        var result = await _repository.AddAsync(entity, cancellationToken);

        return result;
    }

    /// <inheritdoc cref="ISubjectService.AddDefaultAsync(string, CancellationToken)" />
    public async Task<ServiceResult> AddDefaultAsync(string domain, CancellationToken cancellationToken = default)
    {
        var any = await _repository.GetAll().AnyAsync(x => x.Domain == domain, cancellationToken);
        if (any)
        {
            return ServiceResult.Failure();
        }

        var utcNow = DateTime.UtcNow;

        var entity = new SubjectEntity
        {
            Id = Guid.NewGuid(),
            CreationTime = utcNow,
            UpdateTime = utcNow,
            UserId = UserIdentifiers.AdminId,
            Domain = domain,
            Name = DefaultSubjectName
        };

        var result = await _repository.AddAsync(entity, cancellationToken);

        return result;
    }

    /// <inheritdoc cref="ISubjectService.GetPagedAsync(SubjectFilterDto, CancellationToken)" />
    public async Task<PagedList<SubjectDto>> GetPagedAsync(SubjectFilterDto param, CancellationToken cancellationToken = default)
    {
        var subjectQueryable = _repository.GetAll().AsNoTracking();

        var ordering = OrderingUtil.GetOrdering<SubjectEntity>(param.OrderBy!);

        if (ordering.Any())
        {
            subjectQueryable = subjectQueryable.OrderByProperty(ordering[0]);
        }
        else
        {
            subjectQueryable = subjectQueryable.OrderBy(x => x.Name);
        }

        if (!param.Query.IsNullOrEmpty())
        {
            subjectQueryable = subjectQueryable.Where(x => x.Name.ToLower().StartsWith(param.Query!.ToLower()));
        }

        if (!param.Domain.IsNullOrEmpty())
        {
            subjectQueryable = subjectQueryable.Where(x => x.Domain == param.Domain);
        }

        if (!param.Name.IsNullOrEmpty())
        {
            subjectQueryable = subjectQueryable.Where(x => x.Name.ToLower().StartsWith(param.Name!.ToLower()));
        }

        var totalCount = await subjectQueryable.CountAsync(cancellationToken);

        subjectQueryable = subjectQueryable.Skip((param.PageNumber - 1) * param.PageSize).Take(param.PageSize);

        var companiesList = await subjectQueryable.ToListAsync(cancellationToken);

        var mapped = _mapper.Map<List<SubjectDto>>(companiesList);

        var pagedList = new PagedList<SubjectDto>()
        {
            Items = mapped,
            PageNumber = param.PageNumber,
            PageSize = mapped.Count,
            TotalCount = totalCount,
        };

        return pagedList;
    }

    /// <inheritdoc cref="ISubjectService.GetByIdAsync(Guid, CancellationToken)" />
    public async Task<SubjectDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);

        if (entity == null)
        {
            return null;
        }

        var mapped = _mapper.Map<SubjectDto>(entity);

        return mapped;
    }

    /// <inheritdoc cref="ISubjectService.RemoveAsync(Guid, CancellationToken)" />
    public async Task<ServiceResult> RemoveAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetAll().Include(x => x.Content).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (entity == null || (entity.UserId == Guid.Empty && entity.Name == DefaultSubjectName))
        {
            return ServiceResult.Failure();
        }

        if (entity.Content.Any())
        {
            return ServiceResult.Failure(ErrorDescriber.SubjectInUserErrorMessage());
        }

        entity.DeleteTime = DateTime.UtcNow;

        await _repository.SaveChangesAsync(cancellationToken);

        return ServiceResult.Success();
    }

    /// <inheritdoc cref="ISubjectService.UpdateAsync(Guid, UpdateSubjectDto, CancellationToken)" />
    public async Task<ServiceResult> UpdateAsync(Guid id, UpdateSubjectDto model, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);

        if (entity == null || (entity.UserId == Guid.Empty && entity.Name == DefaultSubjectName))
        {
            return ServiceResult.Failure();
        }

        model.Adapt(entity);
        entity.UpdateTime = DateTime.UtcNow;

        await _repository.SaveChangesAsync(cancellationToken);

        return ServiceResult.Success();
    }
}
