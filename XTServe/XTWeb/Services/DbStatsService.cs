using XTWeb.Models;

namespace XTWeb.Services;

public interface IDbStatsService
{
    Task<List<DbStat>> GetDbStatsAsync();
}

public class DbStatsService : IDbStatsService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<DbStatsService> _logger;

    public DbStatsService(IHttpClientFactory httpClientFactory, ILogger<DbStatsService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<List<DbStat>> GetDbStatsAsync()
    {
        try
        {
            var client = _httpClientFactory.CreateClient("XTServeAPI");
            var response = await client.GetAsync("api/DbStats");
            
            if (response.IsSuccessStatusCode)
            {
                var stats = await response.Content.ReadFromJsonAsync<List<DbStat>>();
                return stats ?? new List<DbStat>();
            }
            else
            {
                _logger.LogError($"Failed to fetch DB stats. Status code: {response.StatusCode}");
                return new List<DbStat>();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching DB stats from API");
            return new List<DbStat>();
        }
    }
}
