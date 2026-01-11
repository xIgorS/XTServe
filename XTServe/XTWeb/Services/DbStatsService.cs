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
            _logger.LogInformation($"Calling API at: {client.BaseAddress}api/DbStats");
            
            var response = await client.GetAsync("api/DbStats");
            
            _logger.LogInformation($"API Response Status: {response.StatusCode}");
            
            if (response.IsSuccessStatusCode)
            {
                var stats = await response.Content.ReadFromJsonAsync<List<DbStat>>();
                _logger.LogInformation($"Successfully retrieved {stats?.Count ?? 0} records");
                return stats ?? new List<DbStat>();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Failed to fetch DB stats. Status code: {response.StatusCode}, Content: {errorContent}");
                return new List<DbStat>();
            }
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, $"HTTP request error fetching DB stats from API. Message: {ex.Message}");
            throw new Exception($"Unable to connect to API: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching DB stats from API");
            throw new Exception($"Error loading data: {ex.Message}", ex);
        }
    }
}
