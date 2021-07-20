using AutoMapper;
using Verification.Api.Dtos;
using Verification.Api.Models;

namespace Verification.Api.Profiles
{
    public class UserProfiles : Profile
    {
        public UserProfiles()
        {
            // source => target
            CreateMap<User, UserReadDto>();
            CreateMap<UserCreateDto, User>();
        }
    }
}