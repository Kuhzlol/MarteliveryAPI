using AutoMapper;
using MarteliveryAPI.Models;
using MarteliveryAPI.Models.DTOs;

namespace MarteliveryAPI.Services.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserAllInfoDTO>();
            CreateMap<User, UserMinimalInfoDTO>();
            CreateMap<UserMinimalInfoDTO, User>();
        }
    }
}
