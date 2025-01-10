namespace DomainSpace.Service;

/// <inheritdoc cref="IContentService" />
public class ContentService : IContentService
{
    private readonly IGenericRepository<ContentEntity> _repository;
    private readonly IContentDapperRepository _dapperRepository;
    private readonly IFileService _fileService;
    private readonly IHubService _hubService;
    private readonly IMapper _mapper;

    public ContentService(IGenericRepository<ContentEntity> repository, IContentDapperRepository dapperRepository, IFileService fileService, IHubService hubService, IMapper mapper)
    {
        _repository = repository;
        _dapperRepository = dapperRepository;
        _fileService = fileService;
        _hubService = hubService;
        _mapper = mapper;
    }

    /// <inheritdoc cref="IContentService.AddAsync(AddContentDto, List{AddFileDto}, CancellationToken)" />
    public async Task<ServiceResult> AddAsync(AddContentDto model, List<AddFileDto> files, CancellationToken cancellationToken = default)
    {
        var entity = _mapper.Map<ContentEntity>(model);
        entity.Id = Guid.NewGuid();
        entity.CreationTime = entity.UpdateTime = DateTime.UtcNow;

        if (model.Text.IsNullOrEmpty())
        {
            entity.Text = "";
        }

        foreach (var file in files)
        {
            file.OwnerType = nameof(ContentEntity);
            file.OwnerId = entity.Id;
        }

        if (files.Any())
        {
            var fileResult = await _fileService.AddAsync(model.Domain ?? "Unknown", files, cancellationToken);

            if (!fileResult.IsSuccess)
            {
                return ServiceResult.Failure();
            }
        }

        var result = await _repository.AddAsync(entity, cancellationToken);

        return result;
    }

    /// <inheritdoc cref="IContentService.GetByIdAsync(Guid, Guid, CancellationToken)" />
    public Task<ContentDto?> GetByIdAsync(Guid id, Guid observerId, CancellationToken cancellationToken = default)
    {
        return _dapperRepository.GetByIdAsync(id, observerId, cancellationToken);
    }

    /// <inheritdoc cref="IContentService.GetPagedAsync(ContentFilterDto, CancellationToken)" />
    public Task<PagedList<ContentDto>> GetPagedAsync(ContentFilterDto param, CancellationToken cancellationToken = default)
    {
        return _dapperRepository.GetPagedAsync(param, cancellationToken);
    }

    /// <inheritdoc cref="IContentService.RemoveAsync(DeleteContentDto, CancellationToken)" />
    public async Task<ServiceResult> RemoveAsync(DeleteContentDto model, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(model.Id, cancellationToken);

        if (entity == null || entity.UserId != model.UserId && !model.IsModeratorOrAdmin)
        {
            return ServiceResult.Failure();
        }

        entity.DeleteTime = DateTime.UtcNow;
        await _repository.SaveChangesAsync(cancellationToken);
        await _fileService.RemoveByOwnerAsync(model.Id, nameof(ContentEntity), cancellationToken);

        return ServiceResult.Success();
    }

    /// <inheritdoc cref="IContentService.UpdateAsync(Guid, UpdateContentDto, List{AddFileDto}, CancellationToken)" />
    public async Task<ServiceResult> UpdateAsync(Guid id, UpdateContentDto model, List<AddFileDto> files, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);

        if (entity == null || entity.UserId != model.UserId && !model.IsAdmin)
        {
            return ServiceResult.Failure();
        }

        model.Adapt(entity);
        entity.UpdateTime = DateTime.UtcNow;

        await _repository.SaveChangesAsync(cancellationToken);

        await _fileService.UpdateFilesAsync(new UpdateFilesDto
        {
            OwnerId = entity.Id,
            OwnerType = nameof(ContentEntity),
            NewFiles = files,
            OldFiles = model.OldFiles,
            Domain = entity.Domain
        }, cancellationToken);

        return ServiceResult.Success();
    }

    /// <inheritdoc cref="IContentService.UpdateLikeCountAsync(UpdateContentLikeCountDto, CancellationToken)" />
    public async Task<ServiceResult> UpdateLikeCountAsync(UpdateContentLikeCountDto model, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(model.ContentId, cancellationToken);

        if (entity == null)
        {
            return ServiceResult.Failure();
        }

        entity.LikeCount += model.Count;

        await _repository.SaveChangesAsync(cancellationToken);

        await _hubService.SendLikeCountChangedAsync(new LikeCountChangedDto
        {
            ContentId = model.ContentId,
            Count = entity.LikeCount
        }, cancellationToken);

        return ServiceResult.Success();
    }
}
