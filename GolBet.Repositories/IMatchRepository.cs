using GolBet.Entities;
using GolBet.Entities.Enums;
using GolBet.Repositories.Common;

namespace GolBet.Repositories;

public interface IMatchRepository : IGenericRepository<Match>
{
    Task<IEnumerable<Match>> GetAllWithTeamsAsync();

    Task<Match?> GetByIdWithTeamsAsync(int id);

    Task<IEnumerable<Match>> GetByStatusAsync(MatchStatus status);
}
