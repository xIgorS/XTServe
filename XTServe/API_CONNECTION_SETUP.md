# XTWeb and XTServe API Connection Setup

## Overview
This document describes the configuration to connect XTWeb (Blazor) to XTServe (Web API).

## Configuration Changes Made

### 1. XTServe API (Backend)
- **Port Configuration**: Running on `https://localhost:7001` and `http://localhost:7002`
- **CORS Policy**: Configured to allow requests from XTWeb origins with credentials
  - Allowed Origins: `https://localhost:59336`, `http://localhost:59337`
  - Allows credentials for Windows Authentication
- **Authentication**: Uses Windows Authentication (Negotiate)
- **Authorization**: Requires "AuthorizedUsersOnly" policy

### 2. XTWeb (Blazor Frontend)
- **Port Configuration**: Running on `https://localhost:59336` and `http://localhost:59337`
- **HttpClient Configuration**:
  - Base URL: `https://localhost:7001`
  - Uses default Windows credentials (`UseDefaultCredentials = true`)
  - Accepts self-signed certificates for development

## How to Run Both Projects

### Option 1: Visual Studio Multiple Startup Projects
1. Right-click on the solution in Solution Explorer
2. Select "Configure Startup Projects..."
3. Choose "Multiple startup projects"
4. Set both **XTServe** and **XTWeb** to "Start"
5. Make sure XTServe is listed first (starts before XTWeb)
6. Click OK and press F5

### Option 2: Run Separately
1. Open two terminal windows
2. In Terminal 1:
   ```bash
   cd C:\Repos\XTServe\XTServe
   dotnet run --project XTServe.csproj
   ```
3. In Terminal 2:
   ```bash
   cd C:\Repos\XTServe\XTServe\XTWeb
   dotnet run --project XTWeb.csproj
   ```

## Troubleshooting

### Issue: "Unable to connect to API"
**Possible Causes:**
- XTServe API is not running
- XTServe is not running on port 7001
- Firewall blocking the connection

**Solutions:**
1. Verify XTServe is running by navigating to `https://localhost:7001/api/DbStats` in a browser
2. Check Windows Firewall settings
3. Review the XTWeb console logs for detailed error messages

### Issue: "401 Unauthorized"
**Possible Causes:**
- Windows Authentication not passing credentials
- User not in authorized list

**Solutions:**
1. Make sure you're running both applications with your Windows account
2. Check the authorized users list in both `Program.cs` files (currently allows `iswin\igorsedykh` and `iswin\igorsedykh1`)
3. Update the authorization policy to include your username

### Issue: "CORS Policy Error"
**Possible Causes:**
- XTWeb running on different port than configured
- Credentials not being sent

**Solutions:**
1. Verify XTWeb is running on ports 59336 or 59337
2. Check browser console for exact CORS error
3. Ensure CORS middleware is called before Authentication in XTServe Program.cs

### Issue: "No data displayed"
**Possible Causes:**
- Database connection issue
- Stored procedure doesn't exist or returns no data
- Authorization failing silently

**Solutions:**
1. Check XTServe logs for database connection errors
2. Test the API endpoint directly: `https://localhost:7001/api/DbStats`
3. Verify connection string in XTServe `appsettings.json`
4. Check that SQL Server is accessible and the `dbo.GetDBStats` stored procedure exists

## Testing the Connection

1. **Test API Directly**: Open browser to `https://localhost:7001/api/DbStats`
   - You should be prompted for Windows credentials (if not already authenticated)
   - Should return JSON data with database statistics

2. **Test from XTWeb**: Navigate to `https://localhost:59336/dbstats`
   - Should display data grid with database statistics
   - Check browser console (F12) for any JavaScript errors
   - Check XTWeb application logs for detailed error messages

## Development Environment Notes

- Both projects are configured for .NET 10
- Self-signed certificates are accepted in development mode
- Windows Authentication requires running on Windows with domain/local accounts
- The API expects a SQL Server database with the `dbo.GetDBStats` stored procedure

## Security Notes

?? **Important**: The current configuration accepts any SSL certificate for development purposes. Remove this in production:

```csharp
// Remove this line for production:
ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
```

?? **Authorization**: Currently hardcoded to specific users. Consider using:
- Active Directory groups
- Role-based authorization
- Configuration-based user lists
