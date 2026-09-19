using GolBet.Entities.Common;

namespace GolBet.Entities;

public class Team : AuditableEntity
{
    public string Name { get; set; } = string.Empty;

    public string? Country { get; set; }

    public string? ShieldUrl { get; set; }

    public ICollection<Match> HomeMatches { get; set; } = new List<Match>();

    public ICollection<Match> AwayMatches { get; set; } = new List<Match>();
}
