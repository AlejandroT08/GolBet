using GolBet.Entities.Enums;
using GolBet.Services.Dtos;
using GolBet.Services.DTOs;

namespace GolBet.Services;

public interface IMatchService
{
    Task<IEnumerable<MatchDto>> GetAllAsync(MatchStatus? status = null, string? search = null);

    Task<IEnumerable<MatchDto>> GetByStatusAsync(MatchStatus status);

    Task<MatchDto?> GetByIdAsync(int id);

    Task<MatchDetailDto?> GetDetailAsync(int id);

    Task StartMatchAsync(int id);

    Task<ResolutionSummaryDto> CloseMatchAsync(MatchResultFormDto dto);
}
