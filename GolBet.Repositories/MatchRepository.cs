using GolBet.Entities;
using GolBet.Entities.Enums;
using GolBet.Repositories.Common;
using GolBet.Repositories.Data;
using Microsoft.EntityFrameworkCore;


namespace GolBet.Repositories;

public class MatchRepository : GenericRepository<Match>, IMatchRepository
{
    public MatchRepository(GolBetDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Match>> GetAllWithTeamsAsync() =>
        await _dbSet
            .Include(m => m.HomeTeam)
            .Include(m => m.AwayTeam)
            .Include(m => m.Bets)
            .AsNoTracking()
            .OrderBy(m => m.MatchDate)
            .ToListAsync();

    public async Task<Match?> GetByIdWithTeamsAsync(int id) =>
        await _dbSet
            .Include(m => m.HomeTeam)
            .Include(m => m.AwayTeam)
            .Include(m => m.Bets)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id);

    public async Task<IEnumerable<Match>> GetByStatusAsync(MatchStatus status) =>
        await _dbSet
            .Include(m => m.HomeTeam)
            .Include(m => m.AwayTeam)
            .Include(m => m.Bets)
            .AsNoTracking()
            .Where(m => m.Status == status)
            .OrderBy(m => m.MatchDate)
            .ToListAsync();
}

