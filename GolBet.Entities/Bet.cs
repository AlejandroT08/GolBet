using System.ComponentModel.DataAnnotations.Schema;
using GolBet.Entities.Common;
using GolBet.Entities.Enums;

namespace GolBet.Entities;

public class Bet : AuditableEntity
{
    public int MatchId { get; set; }
    public Match Match { get; set; } = null!;

    public string UserId { get; set; } = null!;   // FK -> AspNetUsers (string PK)
    public AppUser User { get; set; } = null!;    // navigation property

    public BetPick Pick { get; set; }

    public decimal Amount { get; set; }

    public decimal OddsAtPlacement { get; set; }

    public BetStatus Status { get; set; } = BetStatus.Pending;

    // Compatibility properties:
    [NotMapped]
    public string SelectedOutcome
    {
        get => Pick.ToString();
        set
        {
            if (Enum.TryParse<BetPick>(value, true, out var p))
                Pick = p;
        }
    }

    [NotMapped]
    public decimal OddsAtBetTime
    {
        get => OddsAtPlacement;
        set => OddsAtPlacement = value;
    }
}
