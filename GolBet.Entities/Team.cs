using System.ComponentModel.DataAnnotations.Schema;
using GolBet.Entities.Common;

namespace GolBet.Entities;

public class Team : AuditableEntity
{
    public string Name { get; set; } = string.Empty;

    public string? City { get; set; }

    public string? CrestUrl { get; set; }

    // Compatibility properties:
    [NotMapped]
    public string? Country
    {
        get => City;
        set => City = value;
    }

    [NotMapped]
    public string? ShieldUrl
    {
        get => CrestUrl;
        set => CrestUrl = value;
    }

    public ICollection<Match> HomeMatches { get; set; } = new List<Match>();

    public ICollection<Match> AwayMatches { get; set; } = new List<Match>();
}
