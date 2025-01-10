namespace DomainSpace.Service;

/// <inheritdoc cref="ILikeService" />
public class LikeService : ILikeService
{
    private readonly IGenericRepository<LikeEntity> _repository;
    private readonly IContentService _contentService;

    public LikeService(IGenericRepository<LikeEntity> repository, IContentService contentService)
    {
        _repository = repository;
        _contentService = contentService;
    }

    /// <inheritdoc cref="ILikeService.DislikeAsync(DislikeDto, CancellationToken)" />
    public async Task<ServiceResult> DislikeAsync(DislikeDto model, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetAll().FirstOrDefaultAsync(x => x.UserId == model.UserId && x.ContentId == model.ContentId, cancellationToken);

        if (entity == null)
        {
            return ServiceResult.Failure();
        }

        var result = await _repository.RemoveAsync(entity, cancellationToken);

        await _contentService.UpdateLikeCountAsync(new UpdateContentLikeCountDto
        {
            ContentId = model.ContentId,
            Count = -1
        }, cancellationToken);

        return result;
    }

    /// <inheritdoc cref="ILikeService.LikeAsync(LikeDto, CancellationToken)" />
    public async Task<ServiceResult> LikeAsync(LikeDto model, CancellationToken cancellationToken = default)
    {
        var any = await _repository.GetAll().AnyAsync(x => x.UserId == model.UserId && x.ContentId == model.ContentId, cancellationToken);

        if (any)
        {
            return ServiceResult.Failure();
        }

        var utcNow = DateTime.UtcNow;

        var entity = new LikeEntity
        {
            Id = Guid.NewGuid(),
            ContentId = model.ContentId,
            CreationTime = utcNow,
            UpdateTime = utcNow,
            UserId = model.UserId
        };

        var result = await _repository.AddAsync(entity, cancellationToken);

        await _contentService.UpdateLikeCountAsync(new UpdateContentLikeCountDto
        {
            ContentId = model.ContentId,
            Count = 1
        }, cancellationToken);

        return result;
    }
}
