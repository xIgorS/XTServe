namespace XTWeb.Models;

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
