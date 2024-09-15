using AllStars.API.DTO.Dutch;
using AllStars.Domain.Dutch.Models;
using AllStars.Domain.Dutch.Models.Commands;
using AutoMapper;

namespace AllStars.API.Profiles;

public class DutchScoreResponseProfile : Profile
{
    public DutchScoreResponseProfile()
    {
        CreateMap<DutchScore, DutchScoreResponse>()
            .ForMember(
                dest => dest.NickName, 
                opt => opt.MapFrom(src => src.Player.Nickname));
    }
}