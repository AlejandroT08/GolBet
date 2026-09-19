using GolBet.Entities;
using GolBet.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace GolBet.Repositories.Data.Seed;

public static class DbSeeder
{
    public static void Seed(GolBetDbContext context)
    {
        context.Database.Migrate();

        if (context.Teams.Any())
        {
            return;
        }

        var teams = new List<Team>
        {
            new() { Name = "Atlético Nacional", Country = "Colombia" },
            new() { Name = "Millonarios FC", Country = "Colombia" },
            new() { Name = "Deportivo Cali", Country = "Colombia" },
            new() { Name = "Envigado FC", Country = "Colombia" },
            new() { Name = "América de Cali", Country = "Colombia" },
            new() { Name = "Independiente Medellín", Country = "Colombia" },
            new() { Name = "Once Caldas", Country = "Colombia" },
            new() { Name = "Deportes Tolima", Country = "Colombia" },
        };

        context.Teams.AddRange(teams);
        context.SaveChanges();

        var matches = new List<Match>
        {
            new()
            {
                HomeTeamId = teams[0].Id, AwayTeamId = teams[1].Id,
                MatchDate = DateTime.Now.AddDays(2), Status = MatchStatus.Programado,
                HomeOdds = 1.85m, DrawOdds = 3.20m, AwayOdds = 4.10m
            },
            new()
            {
                HomeTeamId = teams[2].Id, AwayTeamId = teams[3].Id,
                MatchDate = DateTime.Now.AddDays(3), Status = MatchStatus.Programado,
                HomeOdds = 2.10m, DrawOdds = 3.00m, AwayOdds = 3.40m
            },
            new()
            {
                HomeTeamId = teams[4].Id, AwayTeamId = teams[5].Id,
                MatchDate = DateTime.Now.AddDays(-1), Status = MatchStatus.Finalizado,
                HomeScore = 2, AwayScore = 1,
                HomeOdds = 1.95m, DrawOdds = 3.10m, AwayOdds = 3.80m
            },
            new()
            {
                HomeTeamId = teams[6].Id, AwayTeamId = teams[7].Id,
                MatchDate = DateTime.Now.AddDays(-2), Status = MatchStatus.Finalizado,
                HomeScore = 0, AwayScore = 0,
                HomeOdds = 2.40m, DrawOdds = 2.90m, AwayOdds = 2.95m
            },
            new()
            {
                HomeTeamId = teams[1].Id, AwayTeamId = teams[4].Id,
                MatchDate = DateTime.Now.AddHours(1), Status = MatchStatus.EnJuego,
                HomeScore = 1, AwayScore = 1,
                HomeOdds = 2.00m, DrawOdds = 3.05m, AwayOdds = 3.50m
            },
            new()
            {
                HomeTeamId = teams[3].Id, AwayTeamId = teams[6].Id,
                MatchDate = DateTime.Now.AddDays(5), Status = MatchStatus.Programado,
                HomeOdds = 1.70m, DrawOdds = 3.30m, AwayOdds = 4.50m
            },
        };

        context.Matches.AddRange(matches);
        context.SaveChanges();
    }
}
