# HealthMed

### Migrations
* Create migration
dotnet ef migrations add <MigrationName> -p src/Infrastructure -s src/Presentation -o Persistence/Migrations

* Apply migration
dotnet ef database update -p "src/Infrastructure" -s "src/Presentation"

### Run
dotnet watch --not-hot-reload --project "src/Presentation"