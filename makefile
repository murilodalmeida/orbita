INFRA_PROJECT="apps/backend/src/Infra/Infra.csproj"
MIGRATIONS_DIR="Data/Migrations"

db-init:
	 dotnet ef migrations add InitDatabase --project $(INFRA_PROJECT) -o $(MIGRATIONS_DIR)

db-script:
	 dotnet ef migrations script --project $(INFRA_PROJECT)

db-update:
	 dotnet ef database update --project $(INFRA_PROJECT)

db-drop:
	 dotnet ef database drop --project $(INFRA_PROJECT)
