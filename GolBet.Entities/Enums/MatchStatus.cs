namespace GolBet.Entities.Enums;

public enum MatchStatus
{
    Scheduled = 0,
    InProgress = 1,
    Finished = 2,
    Cancelled = 3,

    // Aliases for compatibility:
    Programado = Scheduled,
    EnJuego = InProgress,
    Finalizado = Finished,
    Cancelado = Cancelled
}
