# Petrol Training Management

This repository contains a Windows Forms application for managing training programs, employees, departments and related finances.

## Features
- Manage employees and departments
- Track training programs and places
- Record finances and generate reports

## Requirements
- **Visual Studio 2019** or later
- **.NET Framework 4.8**
- SQL Server instance for the application database

## Getting Started
1. Open `Petrol.sln` in Visual Studio.
2. Restore NuGet packages when prompted.
3. Review and update the connection string in `Petrol/Data/AppDbContext.cs` to match your SQL Server.
4. Build and run the project (F5).

The application uses packages listed in `packages.config`. Visual Studio will restore them automatically.

`Petrol.rar` is a packaged copy of the project and can be ignored.
