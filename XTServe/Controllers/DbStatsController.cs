using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace XTServe.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "AuthorizedUsersOnly")]
public class DbStatsController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<DbStatsController> _logger;

    public DbStatsController(IConfiguration configuration, ILogger<DbStatsController> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetDbStats()
    {
        try
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            
            if (string.IsNullOrEmpty(connectionString))
            {
                return StatusCode(500, "Connection string not configured");
            }

            var dbStats = new List<DbStat>();

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                var query = @"SELECT [DatabaseName], [LogicalFileName], [FileGroup], [PhysicalFileName], 
                             [FileType], [AllocatedSpaceMB], [UsedSpaceMB], [FreeSpaceMB], [UsedPercent] 
                             FROM [Log].[dbo].[dbstats]";

                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var stat = new DbStat
                            {
                                DatabaseName = reader["DatabaseName"]?.ToString(),
                                LogicalFileName = reader["LogicalFileName"]?.ToString(),
                                FileGroup = reader["FileGroup"]?.ToString(),
                                PhysicalFileName = reader["PhysicalFileName"]?.ToString(),
                                FileType = reader["FileType"]?.ToString(),
                                AllocatedSpaceMB = reader["AllocatedSpaceMB"] != DBNull.Value 
                                    ? Convert.ToDecimal(reader["AllocatedSpaceMB"]) 
                                    : 0,
                                UsedSpaceMB = reader["UsedSpaceMB"] != DBNull.Value 
                                    ? Convert.ToDecimal(reader["UsedSpaceMB"]) 
                                    : 0,
                                FreeSpaceMB = reader["FreeSpaceMB"] != DBNull.Value 
                                    ? Convert.ToDecimal(reader["FreeSpaceMB"]) 
                                    : 0,
                                UsedPercent = reader["UsedPercent"] != DBNull.Value 
                                    ? Convert.ToDecimal(reader["UsedPercent"]) 
                                    : 0
                            };
                            dbStats.Add(stat);
                        }
                    }
                }
            }

            _logger.LogInformation($"Retrieved {dbStats.Count} database statistics records for user {User.Identity?.Name}");
            
            return Ok(dbStats);
        }
        catch (SqlException ex)
        {
            _logger.LogError(ex, "SQL error occurred while retrieving database statistics");
            return StatusCode(500, $"Database error: {ex.Message}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving database statistics");
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}

public class DbStat
{
    public string? DatabaseName { get; set; }
    public string? LogicalFileName { get; set; }
    public string? FileGroup { get; set; }
    public string? PhysicalFileName { get; set; }
    public string? FileType { get; set; }
    public decimal AllocatedSpaceMB { get; set; }
    public decimal UsedSpaceMB { get; set; }
    public decimal FreeSpaceMB { get; set; }
    public decimal UsedPercent { get; set; }
}
