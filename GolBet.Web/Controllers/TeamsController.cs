using GolBet.Repositories.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GolBet.Web.Controllers;

[Authorize(Roles = DbSeeder.AdminRole)]
public class TeamsController : Controller
{
    private readonly AppDbContext _context;

    public TeamsController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var teams = await _context.Teams
            .OrderBy(t => t.Name)
            .AsNoTracking()
            .ToListAsync();
        return View(teams);
    }
}
