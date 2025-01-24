namespace DomainSpace.Service;

/// <inheritdoc cref="IJokeService" />
public class JokeService : IJokeService
{
    private const string JokeApiUrl = "https://v2.jokeapi.dev/joke/Programming";

    /// <inheritdoc cref="IJokeService.GetAsync(CancellationToken)" />
    public async Task<JokeDto?> GetAsync(CancellationToken cancellationToken = default)
    {
        var url = $"{JokeApiUrl}?blacklistFlags=nsfw,religious,political,racist,sexist,explicit&type=single";

        using var httpClient = new HttpClient();

        var response = await httpClient.GetAsync(url, cancellationToken);

        if (response.StatusCode != HttpStatusCode.OK)
        {
            return null;
        }

        var jsonResponse = await response.Content.ReadAsStringAsync(cancellationToken);
        var result = JsonSerializer.Deserialize<JokeDto>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return result;
    }
}
