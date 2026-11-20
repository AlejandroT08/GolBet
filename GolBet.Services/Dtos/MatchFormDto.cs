using System.ComponentModel.DataAnnotations;

namespace GolBet.Services.DTOs;

public class MatchFormDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El equipo local es obligatorio")]
    [Display(Name = "Equipo local")]
    public int HomeTeamId { get; set; }

    [Required(ErrorMessage = "El equipo visitante es obligatorio")]
    [Display(Name = "Equipo visitante")]
    public int AwayTeamId { get; set; }

    [Required(ErrorMessage = "La fecha es obligatoria")]
    [Display(Name = "Fecha y hora")]
    public DateTime Date { get; set; } = DateTime.UtcNow.AddDays(1);

    [Display(Name = "Cuota local")]
    [Range(1.01, 100.0, ErrorMessage = "La cuota debe ser mayor a 1.00")]
    public decimal HomeOdds { get; set; } = 2.00m;

    [Display(Name = "Cuota empate")]
    [Range(1.01, 100.0, ErrorMessage = "La cuota debe ser mayor a 1.00")]
    public decimal DrawOdds { get; set; } = 3.00m;

    [Display(Name = "Cuota visitante")]
    [Range(1.01, 100.0, ErrorMessage = "La cuota debe ser mayor a 1.00")]
    public decimal AwayOdds { get; set; } = 2.00m;
}
