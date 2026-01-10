# XTServe

XTServe is a .NET 10 Web API that provides database statistics endpoints with Windows Authentication and CORS support.

## Features

- **Windows Authentication**: Uses Negotiate/NTLM authentication
- **CORS Enabled**: Allows requests from all origins
- **SQL Server Integration**: Uses SqlClient for database connectivity
- **DbStats Controller**: Provides database statistics from SQL Server

## API Endpoints

### GET /api/DbStats

Returns database statistics from the `[Log].[dbo].[dbstats]` table.

**Authentication**: Windows Authentication (user: iswin\igorsedykh)

**Response Model**:
```json
[
  {
    "databaseName": "string",
    "logicalFileName": "string",
    "fileGroup": "string",
    "physicalFileName": "string",
    "fileType": "string",
    "allocatedSpaceMB": 0.0,
    "usedSpaceMB": 0.0,
    "freeSpaceMB": 0.0,
    "usedPercent": 0.0
  }
]
```

## Configuration

Update `appsettings.json` with your SQL Server connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SQL_SERVER;Database=Log;Integrated Security=true;TrustServerCertificate=true;"
  }
}
```

## Running the Application

```bash
cd XTServe
dotnet run
```

The API will be available at `http://localhost:5153`

## Building the Application

```bash
cd XTServe
dotnet build
```

## Technologies Used

- .NET 10
- ASP.NET Core Web API
- Microsoft.Data.SqlClient
- Microsoft.AspNetCore.Authentication.Negotiate (Windows Authentication)
