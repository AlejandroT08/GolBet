# GolBet

Aplicación web para consultar una cartelera de partidos de fútbol, con filtros por estado y vista de detalle por partido. Construida en .NET 8 con arquitectura en capas (N-Capas).

## Arquitectura

```
GolBet.Web           → ASP.NET Core MVC (Controllers, Views, Program.cs)
GolBet.Services      → DTOs, AutoMapper, lógica de aplicación (MatchService)
GolBet.Repositories  → DbContext, Fluent API, repositorio genérico, Seeder
GolBet.Entities      → Entidades de dominio (Team, Match, Bet) y enums
```

Referencias entre proyectos: `Web → Services → Repositories → Entities`.

## Stack técnico

- .NET 8 / ASP.NET Core MVC
- Entity Framework Core 8 (Code First) + SQL Server
- AutoMapper 13
- Bootstrap 5

## Modelo de datos

- **Team**: equipos (nombre único, país, escudo opcional).
- **Match**: partido entre dos equipos (`HomeTeam` / `AwayTeam`), fecha, estado (`Programado`, `EnJuego`, `Finalizado`, `Cancelado`), marcador y cuotas (`HomeOdds`, `DrawOdds`, `AwayOdds`).
- **Bet**: apuesta asociada a un partido (resultado elegido, monto, cuota al momento de apostar).

Todas las entidades heredan de `AuditableEntity` (`Id`, `CreatedAt`, `UpdatedAt`), que se estampa automáticamente en `SaveChanges`.

Reglas de Fluent API relevantes (`GolBetDbContext`):
- Índice único sobre `Team.Name`.
- FK de `Match` hacia `HomeTeam` y `AwayTeam` con `DeleteBehavior.Restrict`.
- Cuotas y montos como `decimal` con precisión fija.

## Requisitos previos

- .NET 8 SDK
- SQL Server LocalDB (incluido con Visual Studio) o una instancia de SQL Server
- Herramienta `dotnet-ef`:

```bash
dotnet tool install --global dotnet-ef
```

## Puesta en marcha

Desde la carpeta raíz del repositorio:

```bash
dotnet restore
dotnet build GolBet.sln
```

Generar la migración inicial (crea `GolBet.Repositories/Migrations`):

```bash
dotnet ef migrations add InitialCreate --project GolBet.Repositories --startup-project GolBet.Web
```

Ejecutar la aplicación:

```bash
dotnet run --project GolBet.Web
```

Al arrancar, `Program.cs` aplica las migraciones pendientes (`Database.Migrate()`) y `DbSeeder` puebla la base con 8 equipos y 6 partidos de ejemplo si está vacía. La siembra es idempotente: correr la app varias veces no duplica datos.

Cadena de conexión por defecto (`appsettings.json`):

```
Server=(localdb)\MSSQLLocalDB;Database=GolBetDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True
```

En Visual Studio: abrir `GolBet.sln`, marcar **GolBet.Web** como proyecto de inicio (clic derecho → *Set as Startup Project*) y presionar F5.

## Funcionalidad

- **`/`** — Página de inicio.
- **`/Matches`** — Cartelera de partidos, con tarjetas (escudos o placeholder, fecha, badge de estado en español, marcador si aplica, cuotas 1 X 2) y filtro por estado vía querystring (`?status=Programado`, `EnJuego`, `Finalizado`).
- **`/Matches/Detail/{id}`** — Detalle del partido: breadcrumb, escudos, cuotas nombradas, contador de apuestas y botón de apuesta (deshabilitado para partidos en estado `Programado`). Un `id` inexistente responde `404`.

## Estructura de carpetas

```
GolBet/
├── GolBet.sln
├── GolBet.Entities/
│   ├── Common/AuditableEntity.cs
│   ├── Enums/MatchStatus.cs
│   ├── Team.cs / Match.cs / Bet.cs
├── GolBet.Repositories/
│   ├── Data/GolBetDbContext.cs
│   ├── Data/Seed/DbSeeder.cs
│   ├── Common/IGenericRepository.cs / GenericRepository.cs
│   ├── IMatchRepository.cs / MatchRepository.cs
├── GolBet.Services/
│   ├── Dtos/MatchDto.cs
│   ├── Mapping/MappingProfile.cs
│   ├── Helpers/MatchStatusExtensions.cs
│   ├── IMatchService.cs / MatchService.cs
└── GolBet.Web/
    ├── Program.cs
    ├── Controllers/HomeController.cs / MatchesController.cs
    └── Views/
```