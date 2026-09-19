using GolBet.Entities.Enums;

namespace GolBet.Services.Dtos;

public class MatchDto
{
    public int Id { get; set; }

    public string HomeTeamName { get; set; } = string.Empty;
    public string? HomeTeamShieldUrl { get; set; }

    public string AwayTeamName { get; set; } = string.Empty;
    public string? AwayTeamShieldUrl { get; set; }

    public DateTime MatchDate { get; set; }

    public MatchStatus Status { get; set; }

    public int? HomeScore { get; set; }
    public int? AwayScore { get; set; }

    public decimal HomeOdds { get; set; }
    public decimal DrawOdds { get; set; }
    public decimal AwayOdds { get; set; }

    public int BetsCount { get; set; }
}
