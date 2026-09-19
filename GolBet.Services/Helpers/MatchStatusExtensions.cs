using GolBet.Entities.Enums;

namespace GolBet.Services.Helpers;

public static class MatchStatusExtensions
{
    public static string ToSpanishLabel(this MatchStatus status) => status switch
    {
        MatchStatus.Programado => "Programado",
        MatchStatus.EnJuego => "En Juego",
        MatchStatus.Finalizado => "Finalizado",
        MatchStatus.Cancelado => "Cancelado",
        _ => status.ToString()
    };

    public static string ToBadgeCssClass(this MatchStatus status) => status switch
    {
        MatchStatus.Programado => "bg-primary",
        MatchStatus.EnJuego => "bg-danger",
        MatchStatus.Finalizado => "bg-success",
        MatchStatus.Cancelado => "bg-secondary",
        _ => "bg-dark"
    };
}

