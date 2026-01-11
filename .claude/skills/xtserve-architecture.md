# XTServe Architecture Skill

This skill provides comprehensive context about the XTServe project architecture and design patterns.

## Project Overview

XTServe is a .NET 10 Web API that serves database statistics with Windows Authentication.

## Key Components

### Authentication Layer
- **Windows Authentication**: Uses Negotiate/NTLM protocol
- **Authorization Policy**: Restricts access to specific Windows users
- User validation in authorization policy

### API Layer
- **Controllers**: RESTful API endpoints in the Controllers directory
- **DbStatsController**: Main endpoint for database statistics

### Data Access
- **SQL Server**: Direct SqlClient integration
- **Integrated Security**: Uses Windows credentials for database connections
- **Database**: Connects to `Log` database

### Cross-Origin Resource Sharing (CORS)
- Configured to allow all origins, methods, and headers
- Policy name: "AllowAll"

## Design Patterns

- Minimal API approach with controllers
- Dependency injection for services
- Configuration-based connection strings
- Policy-based authorization

## File Structure

```
XTServe/
├── Program.cs              # Application startup and configuration
├── Controllers/            # API endpoint controllers
│   └── DbStatsController.cs
├── appsettings.json        # Production configuration
└── appsettings.Development.json  # Development configuration
```
