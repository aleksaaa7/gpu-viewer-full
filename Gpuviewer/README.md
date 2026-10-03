Web application for compare a GPU specs (C# / ASP.NET Core + PostgreSQL).
## Technologies
- ASP.NET Core 8 Web API
- Entity Framework Core + PostgreSQL
- Architecture: Repository / Service / Controller layers
## local run
1. install [PostgreSQL 17+](https://www.postgresql.org/download/) and make empty database 'gpuviewer_db'
2. Clone a repo and open `Gpuviewer.sln` in Visual studio 2022+
3. Setup your conn string
-Right click on project and `Gpuviewer` → **Manage User Secrets**
-json
     {
       "ConnectionStrings": {
         "DefaultConnection": "Host=localhost;Port=5432;Database=gpuviewer_db;Username=postgres;Password=YOURPASSWORD"
       }
     }
-in a field where is "YOURPASSWORD" enter a password for a postgresql
4. Run EF Core migration (Package Manager Console): `Update-Database`
5. Run app (F5), and open Swagger.
6. Import seed data: take `POST /api/Seed/import`.
## Project Status
-  Backend: CRUD operations for graphics cards, specification comparison (Compare)
-  In progress: React frontend, login/registration system with admin and guest roles