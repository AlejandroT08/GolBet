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

    public async Task<IEnumerable<Match>> GetAllWithTeamsAsync(MatchStatus? status = null, string? search = null)
    {
        IQueryable<Match> query = _dbSet
            .Include(m => m.HomeTeam)
            .Include(m => m.AwayTeam)
            .Include(m => m.Bets)
            .AsNoTracking();

        if (status.HasValue)
        {
            query = query.Where(m => m.Status == status.Value);
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            var trimmed = search.Trim();
            query = query.Where(m => m.HomeTeam.Name.Contains(trimmed) || m.AwayTeam.Name.Contains(trimmed));
        }

        return await query
            .OrderBy(m => m.MatchDate)
            .ToListAsync();
    }

    public async Task<Match?> GetByIdWithTeamsAsync(int id) =>
        await _dbSet
            .Include(m => m.HomeTeam)
            .Include(m => m.AwayTeam)
            .Include(m => m.Bets)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id);

    public async Task<IEnumerable<Match>> GetByStatusAsync(MatchStatus status) =>
        await GetAllWithTeamsAsync(status);
}

