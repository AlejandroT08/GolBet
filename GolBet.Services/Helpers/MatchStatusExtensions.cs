using GolBet.Entities.Enums;

namespace GolBet.Services.Helpers;

public static class MatchStatusExtensions
{
    public static string ToSpanishLabel(this MatchStatus status) => status switch
    {
        MatchStatus.Scheduled => "Programado",
        MatchStatus.InProgress => "En juego",
        MatchStatus.Finished => "Finalizado",
        MatchStatus.Cancelled => "Cancelado",
        _ => status.ToString()
    };

    public static string ToBadgeCssClass(this MatchStatus status) => status switch
    {
        MatchStatus.Scheduled => "bg-primary",
        MatchStatus.InProgress => "bg-warning text-dark",
        MatchStatus.Finished => "bg-secondary",
        MatchStatus.Cancelled => "bg-dark",
        _ => "bg-secondary"
    };
}
