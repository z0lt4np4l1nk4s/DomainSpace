namespace DomainSpace.Service;

/// <inheritdoc cref="IUserService" />
public class UserService : IUserService
{
    private readonly IUserDapperRepository _userDapperRepository;
    private readonly UserManager<UserEntity> _userManager;
    private readonly IMapper _mapper;

    public UserService(IUserDapperRepository userDapperRepository, UserManager<UserEntity> userManager, IMapper mapper)
    {
        _userDapperRepository = userDapperRepository;
        _userManager = userManager;
        _mapper = mapper;
    }

    /// <inheritdoc cref="IUserService.GetPagedAsync(UserFilterDto, CancellationToken)" />
    public Task<PagedList<UserDto>> GetPagedAsync(UserFilterDto param, CancellationToken cancellationToken = default)
    {
        return _userDapperRepository.GetPagedAsync(param, cancellationToken);
    }

    /// <inheritdoc cref="IUserService.GetByIdAsync(Guid, CancellationToken)" />
    public async Task<UserDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _userManager.FindByIdAsync(id.ToString());

        if (entity == null)
        {
            return null;
        }

        var mapped = _mapper.Map<UserDto>(entity);

        return mapped;
    }

    /// <inheritdoc cref="IUserService.UpdateAsync(Guid, UpdateUserDto, CancellationToken)" />
    public async Task<ServiceResult> UpdateAsync(Guid id, UpdateUserDto model, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());

        if (user == null)
        {
            return ServiceResult.Failure();
        }

        model.Adapt(user);

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            return ServiceResult.Failure();
        }

        return ServiceResult.Success();
    }
}
