using Microsoft.EntityFrameworkCore;

namespace GolBet.Repositories.Data;

// Compatibility alias for AppDbContext
public class GolBetDbContext : AppDbContext
{
    public GolBetDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}
