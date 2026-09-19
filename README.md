# GolBet — Primera Entrega (Módulos 1 a 5)

Arquitectura N-capas en .NET 8:

```
GolBet.Web           → ASP.NET Core MVC (Controllers, Views, Program.cs)
GolBet.Services      → DTOs, AutoMapper, MatchService (lógica de aplicación)
GolBet.Repositories  → DbContext, Fluent API, repositorio genérico, Seeder
GolBet.Entities      → Team, Match, Bet, AuditableEntity, enums
```

## 1. Requisitos previos en tu máquina

- .NET 8 SDK
- SQL Server LocalDB (viene con Visual Studio) o una instancia de SQL Server
- Herramienta `dotnet-ef` (para migraciones):

```bash
dotnet tool install --global dotnet-ef
```

## 2. Restaurar y compilar

```bash
cd GolBet
dotnet restore
dotnet build GolBet.sln
```

## 3. Crear la migración inicial

Este paso **no viene generado** en el código porque las migraciones se crean con tu
propia máquina/SDK. Desde la carpeta raíz `GolBet/`:

```bash
dotnet ef migrations add InitialCreate --project GolBet.Repositories --startup-project GolBet.Web
dotnet ef database update --project GolBet.Repositories --startup-project GolBet.Web
```

Esto crea la carpeta `GolBet.Repositories/Migrations/` (que SÍ debes commitear) y la
base de datos `GolBetDB`. También puedes omitir `database update`: al ejecutar la
aplicación con F5, `Program.cs` llama a `context.Database.Migrate()` y la crea sola.

## 4. Ejecutar

```bash
dotnet run --project GolBet.Web
```

Abre la URL que indique la consola (por ejemplo `http://localhost:5090`). Al arrancar,
el `DbSeeder` puebla la base con 8 equipos y 6 partidos si está vacía (no duplica en
ejecuciones posteriores).

## 5. Verificación rápida contra la checklist (sección 4 de la guía)

- **Módulo 1**: `/` muestra navbar verde, bienvenida y footer con año dinámico.
- **Módulo 2**: revisa `GolBet.Entities` (herencia de `AuditableEntity`) y el Fluent API
  en `GolBetDbContext.OnModelCreating`.
- **Módulo 3**: borra `GolBetDB` en SSMS, corre `dotnet run` de nuevo y confirma que el
  seeder la reconstruye con 8 equipos y 6 partidos, sin duplicar si corres otra vez.
- **Módulo 4**: entra a `/Matches` y revisa las tarjetas (escudos/placeholder, hora
  Colombia, badge de estado, marcador, cuotas 1 X 2).
- **Módulo 5**: prueba los 4 botones de filtro (revisa que la URL cambie a
  `?status=...`), entra al detalle de un partido, y prueba `/Matches/Detail/999`
  (debe responder 404, no una pantalla de excepción).

## 6. Git: cómo dejar un historial honesto

⚠️ **Importante**: la guía de entrega exige un historial de commits **progresivo**,
con al menos un commit + tag por módulo (R3). Si vas a subir este código hoy mismo,
NO simules commits de fechas pasadas: eso vulnera la integridad académica y además es
fácil de detectar. Lo correcto es:

1. Habla con tu profesor si estás arrancando justo el día de la entrega.
2. Si vas a commitear todo hoy, hazlo **en el orden real en que construyes y pruebas
   cada módulo**, un commit + tag por módulo, aunque todos queden en la misma fecha.
   Eso es más honesto que un solo commit gigante.

```bash
git init
git add .gitignore README.md GolBet.sln GolBet.Entities
git commit -m "Modulo 1: solucion base, 4 proyectos y estructura N-capas"
git tag modulo-1

git add GolBet.Entities GolBet.Repositories/Data/GolBetDbContext.cs GolBet.Repositories/GolBet.Repositories.csproj GolBet.Repositories/Migrations
git commit -m "Modulo 2: entidades, Code First y Fluent API"
git tag modulo-2

git add GolBet.Repositories
git commit -m "Modulo 3: repositorio generico, MatchRepository y Seeder"
git tag modulo-3

git add GolBet.Services
git commit -m "Modulo 4: DTOs, AutoMapper, MatchService y cartelera"
git tag modulo-4

git add GolBet.Web
git commit -m "Modulo 5: filtros por querystring y detalle de partido"
git tag modulo-5

git branch -M main
git remote add origin <URL-de-tu-repo-en-GitHub>
git push -u origin main
git push origin --tags
```

Verifica al final con `git tag` (debe listar `modulo-1` a `modulo-5`) y confirma que
el repositorio es **público** abriendo la URL en una ventana de incógnito.
