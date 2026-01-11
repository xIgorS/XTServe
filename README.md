# XTServe

XTServe is a .NET 10 solution containing a Web API and a Blazor Interactive Server web application for viewing database statistics.

## Projects

### XTServe (Web API)

A .NET 10 Web API that provides database statistics endpoints with Windows Authentication and CORS support.

#### Features

- **Windows Authentication**: Uses Negotiate/NTLM authentication
- **CORS Enabled**: Allows requests from all origins
- **SQL Server Integration**: Uses SqlClient for database connectivity
- **DbStats Controller**: Provides database statistics from SQL Server

#### API Endpoints

**GET /api/DbStats**

Returns database statistics from the `dbo.GetDBStats` stored procedure.

**Authentication**: Windows Authentication (user: iswin\igorsedykh or iswin\igorsedykh1)

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

### XTWeb (Blazor Interactive Server)

A Blazor Interactive Server application that provides a user-friendly web interface for viewing database statistics using MudBlazor components.

#### Features

- **MudBlazor UI Components**: Modern Material Design interface
- **Windows Authentication**: Same authentication as the API
- **Interactive DataGrid**: Display and filter database statistics
- **Real-time Updates**: Refresh data on demand
- **Responsive Design**: Works on desktop and mobile devices

#### Pages

- **Home**: Welcome page with navigation
- **DB Statistics**: Interactive data grid displaying database statistics from the API

## Configuration

### XTServe API Configuration

Update `XTServe/appsettings.json` with your SQL Server connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=YOUR_SERVER;Initial Catalog=Log;User ID=sa;Password=YOUR_PASSWORD;..."
  }
}
```

### XTWeb Configuration

Update `XTWeb/appsettings.json` with the API base URL:

```json
{
  "ApiSettings": {
    "BaseUrl": "https://localhost:7001"
  }
}
```

## Running the Applications

### Run the API

```bash
cd XTServe/XTServe
dotnet run
```

The API will be available at `http://localhost:5153` (or the port configured in Properties/launchSettings.json if it exists)

### Run the Web Application

```bash
cd XTServe/XTWeb
dotnet run
```

The web application will be available at:
- HTTPS: `https://localhost:7241`
- HTTP: `http://localhost:5277`

## Building the Applications

### Build Individual Projects

```bash
# Build API
cd XTServe/XTServe
dotnet build

# Build Web App
cd XTServe/XTWeb
dotnet build
```

### Build Solution

Note: Due to cross-compilation issues between projects, it's recommended to build each project separately as shown above.

## Technologies Used

- .NET 10
- ASP.NET Core Web API
- ASP.NET Core Blazor Interactive Server
- MudBlazor 8.15.0
- Microsoft.Data.SqlClient
- Microsoft.AspNetCore.Authentication.Negotiate (Windows Authentication)
