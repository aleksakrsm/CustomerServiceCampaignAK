
commands for creating migrations and updating db

dotnet ef migrations add NEW_MIGRATION -c MyDbContext -p Persistance -s Api -o Migrations
dotnet ef database update -c MyDbContext -p Persistance -s Api
