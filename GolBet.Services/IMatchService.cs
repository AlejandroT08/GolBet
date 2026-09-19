using GolBet.Entities.Enums;
using GolBet.Services.Dtos;

namespace GolBet.Services;

public interface IMatchService
{
    Task<IEnumerable<MatchDto>> GetAllAsync();

    Task<IEnumerable<MatchDto>> GetByStatusAsync(MatchStatus status);

    Task<MatchDto?> GetByIdAsync(int id);
}
