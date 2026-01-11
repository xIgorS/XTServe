# Database Operations Skill

This skill helps with SQL Server database operations in the XTServe project.

## Connection Configuration

The application uses SQL Server with Windows Integrated Security:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SQL_SERVER;Database=Log;Integrated Security=true;TrustServerCertificate=true;"
  }
}
```

### Connection String Components
- **Server**: SQL Server instance name
- **Database**: `Log` database
- **Integrated Security**: Uses Windows Authentication
- **TrustServerCertificate**: Allows self-signed certificates

## Database Statistics

The DbStatsController queries the `[Log].[dbo].[dbstats]` table to retrieve:
- Database name
- Logical file name
- File group
- Physical file name
- File type
- Allocated space (MB)
- Used space (MB)
- Free space (MB)
- Used percentage

## Best Practices

1. **Error Handling**: Always wrap database operations in try-catch blocks
2. **Connection Management**: Use `using` statements for proper disposal
3. **Parameterized Queries**: Prevent SQL injection attacks
4. **Async Operations**: Use async/await for database calls to improve scalability
5. **Connection Pooling**: Enabled by default with SqlClient

## Common Tasks

### Reading Connection String
```csharp
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
```

### Executing Queries
Use SqlClient (`System.Data.SqlClient` or `Microsoft.Data.SqlClient`) for database operations.
