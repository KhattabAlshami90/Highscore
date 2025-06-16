# Highscore Razor Pages Application

This project is a Razor Pages web application built with .NET 7. It manages high scores for games and includes API endpoints.

## Features
- Manage and display high scores
- API endpoints for games and scores
- SQL Server database integration

## Getting Started

### Prerequisites
- [.NET 7 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
- SQL Server (or compatible)

### Setup
1. Clone the repository:
   ```sh
   git clone <repository-url>
   ```
2. Navigate to the project directory:
   ```sh
   cd Highscore
   ```
3. Update the connection string in `appsettings.json` if needed.
4. Apply database migrations:
   ```sh
   dotnet ef database update
   ```
5. Run the application:
   ```sh
   dotnet run
   ```

## Project Structure
- `Pages/` - Razor Pages UI
- `Controllers/` - API controllers
- `Data/` - Database context and seed data
- `appsettings.json` - Configuration

## Development
- Launch profiles are configured in `Properties/launchSettings.json`.
- The default environment is `Development`.

## License
This project is licensed under the MIT License.
