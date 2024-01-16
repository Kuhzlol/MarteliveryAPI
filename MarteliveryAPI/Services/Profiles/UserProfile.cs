using AutoMapper;
using MarteliveryAPI.Models.Domain;
using MarteliveryAPI.Models.DTOs.User;

namespace MarteliveryAPI.Services.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            //Used for getting minimal user info
            CreateMap<User, UserInfoDTO>();
            //Used for updating minimal user info
            CreateMap<UserInfoDTO, User>();
        }
    }
}
