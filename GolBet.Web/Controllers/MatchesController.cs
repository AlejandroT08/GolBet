using GolBet.Entities;
using GolBet.Entities.Enums;
using GolBet.Repositories.Data;
using GolBet.Services;
using GolBet.Services.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GolBet.Web.Controllers;

public class MatchesController : Controller
{
    private readonly IMatchService _matchService;
    private readonly AppDbContext _context;

    public MatchesController(IMatchService matchService, AppDbContext context)
    {
        _matchService = matchService;
        _context = context;
    }

    // GET: /Matches
    public async Task<IActionResult> Index(string? status, string? search)
    {
        MatchStatus? matchStatus = null;

        if (!string.IsNullOrWhiteSpace(status) && Enum.TryParse<MatchStatus>(status, true, out var parsedStatus))
        {
            matchStatus = parsedStatus;
            ViewBag.CurrentStatus = parsedStatus.ToString();
        }
        else
        {
            ViewBag.CurrentStatus = string.Empty;
        }

        ViewBag.CurrentSearch = search ?? string.Empty;

        var matches = await _matchService.GetAllAsync(matchStatus, search);
        return View(matches);
    }

    // GET: /Matches/Detail/5
    public async Task<IActionResult> Detail(int id)
    {
        var match = await _matchService.GetDetailAsync(id);

        if (match is null)
        {
            return NotFound();
        }

        return View(match);
    }

    // GET: /Matches/Create
    [Authorize(Roles = DbSeeder.AdminRole)]
    public async Task<IActionResult> Create()
    {
        await LoadTeamsAsync();
        return View(new MatchFormDto());
    }

    // POST: /Matches/Create
    [HttpPost, ValidateAntiForgeryToken]
    [Authorize(Roles = DbSeeder.AdminRole)]
    public async Task<IActionResult> Create(MatchFormDto dto)
    {
        if (dto.HomeTeamId == dto.AwayTeamId)
        {
            ModelState.AddModelError(nameof(dto.AwayTeamId), "El equipo visitante debe ser diferente al local.");
        }

        if (!ModelState.IsValid)
        {
            await LoadTeamsAsync();
            return View(dto);
        }

        var match = new Match
        {
            HomeTeamId = dto.HomeTeamId,
            AwayTeamId = dto.AwayTeamId,
            Date = dto.Date,
            HomeOdds = dto.HomeOdds,
            DrawOdds = dto.DrawOdds,
            AwayOdds = dto.AwayOdds,
            Status = MatchStatus.Scheduled
        };

        _context.Matches.Add(match);
        await _context.SaveChangesAsync();

        TempData["Success"] = "Partido creado exitosamente.";
        return RedirectToAction(nameof(Index));
    }

    // GET: /Matches/Edit/5
    [Authorize(Roles = DbSeeder.AdminRole)]
    public async Task<IActionResult> Edit(int id)
    {
        var match = await _context.Matches.FindAsync(id);
        if (match is null) return NotFound();

        if (match.Status != MatchStatus.Scheduled)
        {
            TempData["Error"] = "Solo los partidos programados pueden ser editados.";
            return RedirectToAction(nameof(Detail), new { id });
        }

        await LoadTeamsAsync();
        var dto = new MatchFormDto
        {
            Id = match.Id,
            HomeTeamId = match.HomeTeamId,
            AwayTeamId = match.AwayTeamId,
            Date = match.Date,
            HomeOdds = match.HomeOdds,
            DrawOdds = match.DrawOdds,
            AwayOdds = match.AwayOdds
        };

        return View(dto);
    }

    // POST: /Matches/Edit/5
    [HttpPost, ValidateAntiForgeryToken]
    [Authorize(Roles = DbSeeder.AdminRole)]
    public async Task<IActionResult> Edit(int id, MatchFormDto dto)
    {
        if (id != dto.Id) return BadRequest();

        if (dto.HomeTeamId == dto.AwayTeamId)
        {
            ModelState.AddModelError(nameof(dto.AwayTeamId), "El equipo visitante debe ser diferente al local.");
        }

        if (!ModelState.IsValid)
        {
            await LoadTeamsAsync();
            return View(dto);
        }

        var match = await _context.Matches.FindAsync(id);
        if (match is null) return NotFound();

        if (match.Status != MatchStatus.Scheduled)
        {
            TempData["Error"] = "Solo los partidos programados pueden ser editados.";
            return RedirectToAction(nameof(Detail), new { id });
        }

        match.HomeTeamId = dto.HomeTeamId;
        match.AwayTeamId = dto.AwayTeamId;
        match.Date = dto.Date;
        match.HomeOdds = dto.HomeOdds;
        match.DrawOdds = dto.DrawOdds;
        match.AwayOdds = dto.AwayOdds;

        await _context.SaveChangesAsync();

        TempData["Success"] = "Partido actualizado exitosamente.";
        return RedirectToAction(nameof(Detail), new { id });
    }

    // POST /Matches/Start/5
    [HttpPost, ValidateAntiForgeryToken]
    [Authorize(Roles = DbSeeder.AdminRole)]
    public async Task<IActionResult> Start(int id)
    {
        try
        {
            await _matchService.StartMatchAsync(id);
            TempData["Success"] = "¡Partido en juego! Las apuestas quedaron cerradas.";
        }
        catch (InvalidOperationException ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Detail), new { id });
    }

    // GET /Matches/Close/5 -> the score form
    [Authorize(Roles = DbSeeder.AdminRole)]
    public async Task<IActionResult> Close(int id)
    {
        var match = await _matchService.GetDetailAsync(id);
        if (match is null) return NotFound();

        ViewBag.Match = match; // header data for the view
        return View(new MatchResultFormDto { MatchId = id });
    }

    // POST /Matches/Close -> close and resolve
    [HttpPost, ValidateAntiForgeryToken]
    [Authorize(Roles = DbSeeder.AdminRole)]
    public async Task<IActionResult> Close(MatchResultFormDto dto)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Match = await _matchService.GetDetailAsync(dto.MatchId);
            return View(dto);
        }

        try
        {
            var summary = await _matchService.CloseMatchAsync(dto);
            TempData["Success"] =
                $"Partido cerrado {dto.HomeGoals}-{dto.AwayGoals}. " +
                $"{summary.ResolvedBets} apuestas resueltas: {summary.WonBets} ganadas, " +
                $"{summary.TotalPaid:N0} FutCoins pagados.";
        }
        catch (InvalidOperationException ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Detail), new { id = dto.MatchId });
    }

    private async Task LoadTeamsAsync()
    {
        var teams = await _context.Teams.OrderBy(t => t.Name).AsNoTracking().ToListAsync();
        ViewBag.Teams = new SelectList(teams, "Id", "Name");
    }
}
