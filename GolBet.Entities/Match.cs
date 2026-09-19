using GolBet.Entities.Common;
using GolBet.Entities.Enums;

namespace GolBet.Entities;

public class Match : AuditableEntity
{
    public int HomeTeamId { get; set; }
    public Team HomeTeam { get; set; } = null!;

    public int AwayTeamId { get; set; }
    public Team AwayTeam { get; set; } = null!;

    public DateTime MatchDate { get; set; }

    public MatchStatus Status { get; set; } = MatchStatus.Programado;

    public int? HomeScore { get; set; }

    public int? AwayScore { get; set; }

    public decimal HomeOdds { get; set; }

    public decimal DrawOdds { get; set; }

    public decimal AwayOdds { get; set; }

    public ICollection<Bet> Bets { get; set; } = new List<Bet>();
}
