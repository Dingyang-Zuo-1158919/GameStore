# Game Store

## Starting SQL Server docker container
```powershell
$sa_password = "SA PASSWORD HERE"
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=$sa_password"  -p 1433:1433  -d -v sqlvolume:/var/opt/mssql --rm --name mssql mcr.microsoft.com/mssql/server:2022-latest
```


## Setting the connection string in Secret Manager
dotnet user-secrets init(server side)
```powershell
$sa_password = "SA PASSWORD HERE"
dotnet user-secrets set "ConnectionStrings:GameStoreContext" "Server=localhost; Database=GameStore; User Id=sa; Password=$sa_password; TrustServerCertificate=True"
```
dotnet user-secrets list

## EntityFramework install
dotnet add package Microsoft.EntityFrameworkCore.SqlServer(server side)

## EF migration tool install
dotnet tool install --global dotnet-ef

## EF design
dotnet add package Microsoft.EntityFrameworkCore.Design

## EF migration initial
dotnet ef migrations add InitialCreate --output-dir Data/Migrations

## EF database update
dotnet ef database update