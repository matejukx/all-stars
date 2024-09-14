using AllStars.API.DTO.User;
using AllStars.Domain.User.Models;
using AutoMapper;

namespace AllStars.API.Profiles;

public class CreateUserRequestProfile : Profile
{
    public CreateUserRequestProfile()
    {
        CreateMap<CreateUserRequest, AllStarUser>(MemberList.Source);
    }
}