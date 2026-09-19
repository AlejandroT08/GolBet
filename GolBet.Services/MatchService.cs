using AutoMapper;
using GolBet.Entities.Enums;
using GolBet.Repositories;
using GolBet.Services.Dtos;

namespace GolBet.Services;

public class MatchService : IMatchService
{
    private readonly IMatchRepository _matchRepository;
    private readonly IMapper _mapper;

    public MatchService(IMatchRepository matchRepository, IMapper mapper)
    {
        _matchRepository = matchRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MatchDto>> GetAllAsync()
    {
        var matches = await _matchRepository.GetAllWithTeamsAsync();
        return _mapper.Map<IEnumerable<MatchDto>>(matches);
    }

    public async Task<IEnumerable<MatchDto>> GetByStatusAsync(MatchStatus status)
    {
        var matches = await _matchRepository.GetByStatusAsync(status);
        return _mapper.Map<IEnumerable<MatchDto>>(matches);
    }

    public async Task<MatchDto?> GetByIdAsync(int id)
    {
        var match = await _matchRepository.GetByIdWithTeamsAsync(id);
        return match is null ? null : _mapper.Map<MatchDto>(match);
    }
}
