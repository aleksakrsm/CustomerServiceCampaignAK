dotnet ef migrations add InitialMigration -c MyDbContext -p Persistance -s CustomerServiceCampaignAK -o Migrations

dotnet ef database update -c MyDbContext -p Persistence -s CustomerServiceCampaignAK
