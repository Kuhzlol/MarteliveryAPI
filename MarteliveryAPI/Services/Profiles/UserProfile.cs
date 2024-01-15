using AutoMapper;
using MarteliveryAPI.Models;
using MarteliveryAPI.Models.DTOs;

namespace MarteliveryAPI.Services.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            //Used for getting mnimal user info
            CreateMap<User, UserMinimalInfoDTO>();
            //Used for updating minimal user info
            CreateMap<UserMinimalInfoDTO, User>();
        }
    }
}
