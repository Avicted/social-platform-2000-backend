# Forum backend

VSCode development docker container has been setup.

Build & run:
```bash
dotnet build

dotnet run --project social-platform-2000-backend/social-platform-2000-backend.csproj
```
Create a new database migration:
```bash
dotnet-ef migrations Add -o DataAccessLayer/Migrations <name_of_migration>
```

Update the database with the migration(s):
```bash
dotnet-ef database update
```

# Architecture

![Architecture](architecture.png "Architecture")

# Database Diagram

![Database Diagram](db_diagram.png "Database Diagram")