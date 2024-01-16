using AutoMapper;
using MarteliveryAPI.Models.Domain;
using MarteliveryAPI.Models.DTOs.Admin;
using MarteliveryAPI.Models.DTOs.User;

namespace MarteliveryAPI.Services.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, AdminUserDTO>().ReverseMap();
            CreateMap<AdminUserDTO, User>().ReverseMap();
            
            CreateMap<User, UserInfoDTO>().ReverseMap();
            CreateMap<UserInfoDTO, User>().ReverseMap();
        }
    }
}
