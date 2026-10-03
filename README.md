# GPU Viewer

Full-stack web application for comparing graphics card specifications.

## Tech Stack

**Backend:**
- ASP.NET Core 8 Web API
- Entity Framework Core + PostgreSQL
- JWT Authentication (Admin/Guest roles)
- Architecture: Repository / Service / Controller layers

**Frontend:**
- React (Vite)
- Vanilla CSS

## Project Structure
gpu-viewer-full/
├── Gpuviewer/ # .NET backend
└── gpuviewer-frontend/ # React frontend

## Running Locally

### Backend

1. Install [PostgreSQL 17+](https://www.postgresql.org/download/) and create an empty database `gpuviewer_db`.
2. Open `Gpuviewer/Gpuviewer.sln` in Visual Studio 2022+.
3. Set up your connection string via User Secrets:
   - Right click on the `Gpuviewer` project → **Manage User Secrets**
   - Paste (replace `YOURPASSWORD` with your PostgreSQL password):
```json
     {
       "ConnectionStrings": {
         "DefaultConnection": "Host=localhost;Port=5432;Database=gpuviewer_db;Username=postgres;Password=YOURPASSWORD"
       }
     }
```
4. Run EF Core migrations (Package Manager Console): `Update-Database`
5. Run the app (F5).
6. Import seed data: call `POST /api/Seed/import` via Swagger.

> **Note:** For ease of local setup, the JWT signing key is included directly in `appsettings.json`. In a production environment, this should be moved to environment variables or a secrets manager.

### Frontend

1. Open a terminal inside `gpuviewer-frontend/`.
2. Run `npm install`
3. Run `npm run dev`
4. Open `http://localhost:5173/`

> Make sure the backend is running first so the frontend can fetch data.

## Features

- ✅ Browse and search all graphics cards
- ✅ Compare two cards side-by-side (spec-based percentage comparison)
- ✅ JWT login/register with Admin/Guest roles
- ✅ Admin-only: add new graphics cards manually
- In progress: CPU comparison