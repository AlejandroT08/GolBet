using System.ComponentModel.DataAnnotations.Schema;
using GolBet.Entities.Common;
using GolBet.Entities.Enums;

namespace GolBet.Entities;

public class Match : AuditableEntity
{
    public int HomeTeamId { get; set; }
    public Team HomeTeam { get; set; } = null!;

    public int AwayTeamId { get; set; }
    public Team AwayTeam { get; set; } = null!;

    public DateTime Date { get; set; }

    // Compatibility property:
    [NotMapped]
    public DateTime MatchDate
    {
        get => Date;
        set => Date = value;
    }

    public MatchStatus Status { get; set; } = MatchStatus.Scheduled;

    public int? HomeGoals { get; set; }
    public int? AwayGoals { get; set; }

    // Compatibility properties:
    [NotMapped]
    public int? HomeScore
    {
        get => HomeGoals;
        set => HomeGoals = value;
    }

    [NotMapped]
    public int? AwayScore
    {
        get => AwayGoals;
        set => AwayGoals = value;
    }

    public decimal HomeOdds { get; set; }

    public decimal DrawOdds { get; set; }

    public decimal AwayOdds { get; set; }

    public bool IsActive { get; set; } = true;

    public ICollection<Bet> Bets { get; set; } = new List<Bet>();
}
