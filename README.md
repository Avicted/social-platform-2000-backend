# Forum backend

Build & run:
```bash
dotnet build
dotnet run
```
Create a new database migration:
```bash
dotnet-ef migrations Add -o DataAccessLayer/Migrations <name_of_migration>
```

Update the database with the migration(s):
```bash
dotnet-ef database update
```