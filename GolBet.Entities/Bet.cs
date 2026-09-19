using GolBet.Entities.Common;

namespace GolBet.Entities;

public class Bet : AuditableEntity
{
    public int MatchId { get; set; }
    public Match Match { get; set; } = null!;

    public string SelectedOutcome { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public decimal OddsAtBetTime { get; set; }
}
