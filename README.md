# ASimmo

ASimmo is an ASP.NET Core MVC web application for managing real estate properties ("Biens Immobiliers"), addresses, classifications, and related entities. The solution includes both the main web application and a suite of automated tests.

## Project Structure

- **ASimmo/**: Main ASP.NET Core MVC application
    - Controllers, Models, Views, Data, and Identity setup
    - Handles property listings, address management, user authentication, and role-based access
- **ASimmo.Tests/**: Automated test project
    - Functional and smoke tests for controllers and pages
    - Uses xUnit and ASP.NET Core's test server infrastructure

## Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- (Optional) SQLite or your preferred database provider

### Setup
1. **Clone the repository**
2. **Restore dependencies:**
   ```bash
   dotnet restore
   ```
3. **Apply migrations and seed the database:**
   ```bash
   dotnet ef database update
   ```
   The database will be seeded with default users and roles as defined in `appsettings.json` and `Startup.cs`.
4. **Run the application:**
   ```bash
   dotnet run --project ASimmo
   ```
   The app will be available at `https://localhost:5001` or `http://localhost:5000` by default.

### Default Users
Seeded users and roles are defined in `appsettings.json`. Example credentials:
- **Admin**: `admin@email.com` / `P@ssw0rd`
- **Agent**: (see `appsettings.json` for details)

These users can be used to access role-protected endpoints.

## Running Tests

Navigate to the solution root and run:
```bash
dotnet test
```

This will execute all functional and smoke tests in `ASimmo.Tests`. Some tests authenticate as seeded users to verify access to protected endpoints.

## Key Features
- Property (Bien Immo) management
- Address CRUD
- Role-based access control (Admin, Agent, etc.)
- ASP.NET Core Identity integration
- Automated functional and smoke tests

## Notes
- The test infrastructure includes helpers to authenticate as seeded users and access protected endpoints.
- If you add new protected endpoints, update the tests to use the authentication helper as needed.

## License
MIT (or specify your license here)
