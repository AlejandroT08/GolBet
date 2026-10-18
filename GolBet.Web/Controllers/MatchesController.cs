using GolBet.Entities.Enums;
using GolBet.Services;
using Microsoft.AspNetCore.Mvc;

namespace GolBet.Web.Controllers;

public class MatchesController : Controller
{
    private readonly IMatchService _matchService;

    public MatchesController(IMatchService matchService)
    {
        _matchService = matchService;
    }

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

    public async Task<IActionResult> Detail(int id)
    {
        var match = await _matchService.GetByIdAsync(id);

        if (match is null)
        {
            return NotFound();
        }

        return View(match);
    }
}
