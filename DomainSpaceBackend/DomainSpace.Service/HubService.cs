namespace DomainSpace.Service;

/// <inheritdoc cref="IHubService" />
public class HubService : Hub, IHubService
{
    private readonly IHubContext<HubService> _hubContext;

    public HubService(IHubContext<HubService> hubContext)
    {
        _hubContext = hubContext;
    }

    /// <inheritdoc cref="IHubService.SendLikeCountChangedAsync(LikeCountChangedDto, CancellationToken)" />
    public async Task SendLikeCountChangedAsync(LikeCountChangedDto model, CancellationToken cancellationToken = default)
    {
        await _hubContext.Clients.All.SendAsync("LikeCountChanged", model, cancellationToken);
    }
}
