using AllStars.API.DTO.Dutch;
using AllStars.Domain.Dutch.Models.Commands;
using AutoMapper;

namespace AllStars.API.Profiles;

public class CreateDutchGameCommandProfile : Profile
{
    public CreateDutchGameCommandProfile()
    {
        CreateMap<CreateDutchGameRequest, CreateDutchGameCommand>(MemberList.Destination);
    }
}
