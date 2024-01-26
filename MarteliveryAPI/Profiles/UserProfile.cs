using AutoMapper;
using MarteliveryAPI.Models.Domain;
using MarteliveryAPI.Models.DTOs.Admin;
using MarteliveryAPI.Models.DTOs.User;

namespace MarteliveryAPI.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, AdminUserInfoDTO>().ReverseMap();
            CreateMap<AdminUserInfoDTO, User>().ReverseMap();
            CreateMap<AdminUpdateUserDTO, User>().ReverseMap();

            CreateMap<User, GetUserInfoDTO>().ReverseMap();
            CreateMap<PutUserInfoDTO, User>().ReverseMap();
            CreateMap<PutUserPasswordDTO, User>().ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password)).ReverseMap();
        }
    }
}
