# HealthMed
[![codecov](https://codecov.io/gh/JezielMoura/HealthMed/graph/badge.svg?token=D5AOGMWZ0T)](https://codecov.io/gh/JezielMoura/HealthMed)

### Settings
Change Postgresql connection string and smtp server data at appsettings.json

### Apply migrations
dotnet ef database update -p "src/Infrastructure" -s "src/Presentation"

### Run
dotnet run --project "src/Presentation"