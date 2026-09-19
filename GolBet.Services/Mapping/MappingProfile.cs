using AutoMapper;
using GolBet.Entities;
using GolBet.Services.Dtos;

namespace GolBet.Services.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Match, MatchDto>()
            .ForMember(dest => dest.HomeTeamName, opt => opt.MapFrom(src => src.HomeTeam.Name))
            .ForMember(dest => dest.HomeTeamShieldUrl, opt => opt.MapFrom(src => src.HomeTeam.ShieldUrl))
            .ForMember(dest => dest.AwayTeamName, opt => opt.MapFrom(src => src.AwayTeam.Name))
            .ForMember(dest => dest.AwayTeamShieldUrl, opt => opt.MapFrom(src => src.AwayTeam.ShieldUrl))
            .ForMember(dest => dest.BetsCount, opt => opt.MapFrom(src => src.Bets.Count));
    }
}
