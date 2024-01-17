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
            CreateMap<User, AdminUserInfoDTO>().ReverseMap();
            CreateMap<AdminUserInfoDTO, User>().ReverseMap();
            CreateMap<AdminUserUpdateDTO, User>().ReverseMap();

            CreateMap<User, UserInfoDTO>().ReverseMap();
            CreateMap<UserInfoUpdateDTO, User>().ReverseMap();
            CreateMap<UserPasswordUpdateDTO, User>().ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password)).ReverseMap();
        }
    }
}
