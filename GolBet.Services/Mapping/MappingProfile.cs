using AutoMapper;
using GolBet.Entities;
using GolBet.Services.Dtos;
using GolBet.Services.DTOs;

namespace GolBet.Services.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Match, MatchDto>()
            .ForMember(dest => dest.HomeTeamName, opt => opt.MapFrom(src => src.HomeTeam.Name))
            .ForMember(dest => dest.HomeTeamCrestUrl, opt => opt.MapFrom(src => src.HomeTeam.CrestUrl ?? src.HomeTeam.ShieldUrl))
            .ForMember(dest => dest.AwayTeamName, opt => opt.MapFrom(src => src.AwayTeam.Name))
            .ForMember(dest => dest.AwayTeamCrestUrl, opt => opt.MapFrom(src => src.AwayTeam.CrestUrl ?? src.AwayTeam.ShieldUrl))
            .ForMember(dest => dest.TotalBets, opt => opt.MapFrom(src => src.Bets.Count))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
            .ForMember(dest => dest.HomeGoals, opt => opt.MapFrom(src => src.HomeGoals))
            .ForMember(dest => dest.AwayGoals, opt => opt.MapFrom(src => src.AwayGoals));

        CreateMap<Match, MatchDetailDto>()
            .ForMember(dest => dest.HomeTeamName, opt => opt.MapFrom(src => src.HomeTeam.Name))
            .ForMember(dest => dest.HomeTeamCrestUrl, opt => opt.MapFrom(src => src.HomeTeam.CrestUrl ?? src.HomeTeam.ShieldUrl))
            .ForMember(dest => dest.AwayTeamName, opt => opt.MapFrom(src => src.AwayTeam.Name))
            .ForMember(dest => dest.AwayTeamCrestUrl, opt => opt.MapFrom(src => src.AwayTeam.CrestUrl ?? src.AwayTeam.ShieldUrl))
            .ForMember(dest => dest.TotalBets, opt => opt.MapFrom(src => src.Bets.Count))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
            .ForMember(dest => dest.HomeGoals, opt => opt.MapFrom(src => src.HomeGoals))
            .ForMember(dest => dest.AwayGoals, opt => opt.MapFrom(src => src.AwayGoals));

        CreateMap<Bet, BetDto>()
            .ForMember(dto => dto.MatchHomeTeamName, opt => opt.MapFrom(bet => bet.Match.HomeTeam.Name))
            .ForMember(dto => dto.MatchAwayTeamName, opt => opt.MapFrom(bet => bet.Match.AwayTeam.Name))
            .ForMember(dto => dto.MatchDate, opt => opt.MapFrom(bet => bet.Match.Date))
            .ForMember(dto => dto.PotentialPayout,
                       options => options.MapFrom(bet => bet.Amount * bet.OddsAtPlacement));
    }
}
