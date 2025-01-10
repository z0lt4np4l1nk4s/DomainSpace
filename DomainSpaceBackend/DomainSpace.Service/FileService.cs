namespace DomainSpace.Service;

/// <inheritdoc cref="IFileService" />
public class FileService : IFileService
{
    private readonly IGenericRepository<FileEntity> _repository;
    private readonly IMapper _mapper;

    public FileService(IGenericRepository<FileEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <inheritdoc cref="IFileService.AddAsync(string, List{AddFileDto}, CancellationToken)" />
    public async Task<ServiceResult> AddAsync(string domain, List<AddFileDto> models, CancellationToken cancellationToken = default)
    {
        DateTime utcNow = DateTime.UtcNow;

        var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "Public", domain);
        Directory.CreateDirectory(folderPath);

        var entities = new List<FileEntity>();
        foreach (var model in models)
        {
            var entity = _mapper.Map<FileEntity>(model);

            var fileName = $"{StringUtil.GenerateRandomString(6)}{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}.{model.Extension}";

            await File.WriteAllBytesAsync(Path.Combine(folderPath, fileName), model.Content, cancellationToken);

            entity.Id = Guid.NewGuid();
            entity.CreationTime = entity.UpdateTime = utcNow;
            entity.Path = $"Files/{domain}/{fileName}";
            entity.LocalPath = Path.Combine("Files", "Public", domain, fileName);

            entities.Add(entity);
        }

        var result = await _repository.AddRangeAsync(entities, cancellationToken);

        return result;
    }

    /// <inheritdoc cref="IFileService.DownloadAllAsync(Guid, MemoryStream, CancellationToken)" />
    public async Task<ServiceResult> DownloadAllAsync(Guid contentId, MemoryStream zipStream, CancellationToken cancellationToken = default)
    {
        var files = await _repository.GetAll().Where(x => x.OwnerId == contentId && x.OwnerType == nameof(ContentEntity)).ToListAsync(cancellationToken);

        if (!files.Any())
        {
            return ServiceResult.Failure();
        }

        using var archive = new ZipArchive(zipStream, ZipArchiveMode.Create, true);

        foreach (var file in files)
        {
            var entry = archive.CreateEntry(file.Name);
            using var entryStream = entry.Open();
            using var fileStream = System.IO.File.OpenRead(Path.Combine(Directory.GetCurrentDirectory(), file.LocalPath));
            await fileStream.CopyToAsync(entryStream, cancellationToken);
        }

        return ServiceResult.Success();
    }

    /// <inheritdoc cref="IFileService.GetAllAsync(FileFilterDto, CancellationToken)" />
    public async Task<List<FileDto>> GetAllAsync(FileFilterDto param, CancellationToken cancellationToken = default)
    {
        var files = await _repository.GetAll().Where(x => x.OwnerId == param.OwnerId && x.OwnerType == param.OwnerType).ToListAsync(cancellationToken);

        var mapped = _mapper.Map<List<FileDto>>(files);

        return mapped;
    }

    /// <inheritdoc cref="IFileService.RemoveByOwnerAsync(Guid, string, CancellationToken)" />
    public async Task<ServiceResult> RemoveByOwnerAsync(Guid ownerId, string ownerType, CancellationToken cancellationToken = default)
    {
        var files = await _repository.GetAll().Where(x => x.OwnerId == ownerId && x.OwnerType == ownerType).ToListAsync(cancellationToken);

        var utcNow = DateTime.UtcNow;

        foreach (var file in files)
        {
            file.DeleteTime = utcNow;
        }

        await _repository.SaveChangesAsync(cancellationToken);

        return ServiceResult.Success();
    }

    /// <inheritdoc cref="IFileService.UpdateFilesAsync(UpdateFilesDto, CancellationToken)" />s
    public async Task<ServiceResult> UpdateFilesAsync(UpdateFilesDto model, CancellationToken cancellationToken = default)
    {
        var utcNow = DateTime.UtcNow;

        var files = await _repository.GetAll().Where(x => x.OwnerId == model.OwnerId && x.OwnerType == model.OwnerType).ToListAsync(cancellationToken);

        foreach (var file in files)
        {
            if (!model.OldFiles.Any(x => x.FileId == file.Id))
            {
                file.DeleteTime = utcNow;
            }
        }

        if (model.NewFiles.Any())
        {
            await AddAsync(model.Domain, model.NewFiles, cancellationToken);
        }

        return ServiceResult.Success();
    }
}
