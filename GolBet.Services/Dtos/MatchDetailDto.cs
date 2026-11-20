using GolBet.Entities.Enums;

namespace GolBet.Services.DTOs;

public class MatchDetailDto
{
    public int Id { get; set; }

    public string HomeTeamName { get; set; } = string.Empty;
    public string? HomeTeamCrestUrl { get; set; }

    public string AwayTeamName { get; set; } = string.Empty;
    public string? AwayTeamCrestUrl { get; set; }

    public DateTime Date { get; set; }
    public MatchStatus Status { get; set; }

    public int? HomeGoals { get; set; }
    public int? AwayGoals { get; set; }

    public decimal HomeOdds { get; set; }
    public decimal DrawOdds { get; set; }
    public decimal AwayOdds { get; set; }

    public int TotalBets { get; set; }

    // Compatibility aliases:
    public int? HomeScore { get => HomeGoals; set => HomeGoals = value; }
    public int? AwayScore { get => AwayGoals; set => AwayGoals = value; }
    public DateTime MatchDate { get => Date; set => Date = value; }
    public int BetsCount { get => TotalBets; set => TotalBets = value; }
    public string? HomeTeamShieldUrl { get => HomeTeamCrestUrl; set => HomeTeamCrestUrl = value; }
    public string? AwayTeamShieldUrl { get => AwayTeamCrestUrl; set => AwayTeamCrestUrl = value; }
}
