using AutoMapper;
using Verification.Dtos;
using Verification.Models;

namespace Verification.Profiles
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