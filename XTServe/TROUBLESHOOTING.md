# Troubleshooting Guide: DbStats Page Loading Issues

## Quick Diagnostics

### 1. Check if XTServe API is Running
Open a browser and navigate to:
```
https://localhost:7001/api/DbStats
```

**Expected Result:** JSON data with database statistics
**If it fails:** XTServe API is not running or not accessible

### 2. Check Browser Console (F12)
Look for errors in the Console tab:
- SignalR connection errors
- JavaScript errors
- Network request failures

### 3. Check Application Logs

#### XTWeb Logs
Look for these log entries in the XTWeb console/output:
```
Calling API at: https://localhost:7001/api/DbStats
API Response Status: [StatusCode]
Successfully retrieved X records
```

#### XTServe Logs
Look for these log entries in the XTServe console/output:
```
Retrieved X database statistics records for user [username]
```

### 4. Common Issues and Solutions

#### Issue: Page Stuck on "Loading..."
**Symptoms:**
- Spinner keeps rotating
- No error messages displayed
- No data appears

**Possible Causes & Solutions:**

1. **SignalR Connection Failed**
   - Check browser console for SignalR errors
   - Ensure WebSockets are not blocked by firewall/proxy
   - Verify XTWeb is running on correct ports (59336/59337)

2. **API Not Responding**
   - Test API directly: `https://localhost:7001/api/DbStats`
   - Check XTServe is running
   - Review XTServe logs for errors

3. **Exception in Service**
   - Check XTWeb logs for exception details
   - Look for HTTP request errors
   - Verify HttpClient configuration

4. **Authentication Issues**
   - 401 Unauthorized: Check Windows credentials
   - 403 Forbidden: User not in authorized list
   - CORS errors: Check CORS configuration

#### Issue: "Error loading data: Unable to connect to API"
**Causes:**
- XTServe API not running
- Wrong port configuration
- Network/firewall blocking connection
- SSL certificate issues

**Solutions:**
1. Start XTServe first
2. Verify `appsettings.json` has correct API URL
3. Check Windows Firewall settings
4. Ensure SSL certificate is trusted (or dev override is working)

#### Issue: "Error loading data: No data returned from the API"
**Causes:**
- Database connection failed
- Stored procedure doesn't exist
- Stored procedure returns no rows
- Empty result set

**Solutions:**
1. Check XTServe logs for database errors
2. Verify connection string in XTServe `appsettings.json`
3. Test stored procedure directly in SQL Server
4. Ensure SQL Server is accessible

#### Issue: "401 Unauthorized"
**Causes:**
- Windows credentials not being passed
- User not in authorized list

**Solutions:**
1. Check `UseDefaultCredentials = true` in XTWeb Program.cs
2. Verify your username in authorization policy:
   ```csharp
   // Both XTServe and XTWeb Program.cs
   return userName != null && (userName == "iswin\\igorsedykh" || userName == "iswin\\igorsedykh1");
   ```
3. Add your username to the authorized list
4. Or temporarily remove authorization for testing:
   ```csharp
   // Remove [Authorize] attribute from DbStatsController
   ```

### 5. Debug Mode Checklist

**Before starting both projects:**
- [ ] SQL Server is running and accessible
- [ ] Database exists and stored procedure is present
- [ ] Connection string is correct
- [ ] Your Windows username is in the authorized users list

**Start XTServe first:**
- [ ] Check console for "Now listening on: https://localhost:7001"
- [ ] Test API: https://localhost:7001/api/DbStats
- [ ] Should return JSON data (may prompt for credentials)

**Then start XTWeb:**
- [ ] Check console for "Now listening on: https://localhost:59336"
- [ ] Browser should open automatically
- [ ] Navigate to /dbstats page

**On the DbStats page:**
- [ ] Open browser console (F12)
- [ ] Check for any red errors
- [ ] Should see loading spinner briefly
- [ ] Then data grid with statistics

### 6. Enable Detailed Logging

Add to `appsettings.json` in both projects:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.AspNetCore.SignalR": "Debug",
      "Microsoft.AspNetCore.Http.Connections": "Debug",
      "XTWeb.Services": "Debug"
    }
  }
}
```

### 7. Test API Independently

Use PowerShell to test the API:
```powershell
# Test without authentication
Invoke-WebRequest -Uri "https://localhost:7001/api/DbStats" -UseDefaultCredentials

# If that fails, try with explicit credentials
$cred = Get-Credential
Invoke-WebRequest -Uri "https://localhost:7001/api/DbStats" -Credential $cred
```

### 8. Network Troubleshooting

Check if ports are listening:
```powershell
# Check XTServe
netstat -ano | findstr :7001

# Check XTWeb
netstat -ano | findstr :59336
```

Test connectivity:
```powershell
Test-NetConnection -ComputerName localhost -Port 7001
Test-NetConnection -ComputerName localhost -Port 59336
```

### 9. Reset Everything

If all else fails:
```powershell
# Stop both applications
# Clean solution
dotnet clean C:\Repos\XTServe\XTServe\XTServe.sln

# Remove obj/bin folders
Remove-Item -Path "C:\Repos\XTServe\XTServe\obj" -Recurse -Force
Remove-Item -Path "C:\Repos\XTServe\XTServe\bin" -Recurse -Force
Remove-Item -Path "C:\Repos\XTServe\XTServe\XTWeb\obj" -Recurse -Force
Remove-Item -Path "C:\Repos\XTServe\XTServe\XTWeb\bin" -Recurse -Force

# Rebuild
dotnet build C:\Repos\XTServe\XTServe\XTServe.sln

# Start XTServe first, then XTWeb
```

### 10. Still Not Working?

Gather this information:
1. Screenshot of browser console errors (F12)
2. XTServe console output
3. XTWeb console output
4. Result of testing API directly: `https://localhost:7001/api/DbStats`
5. Your Windows username
6. SQL Server connection test result

This will help diagnose the specific issue you're facing.
