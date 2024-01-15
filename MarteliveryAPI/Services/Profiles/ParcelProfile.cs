using AutoMapper;
using MarteliveryAPI.Models;
using MarteliveryAPI.Models.DTOs;

namespace MarteliveryAPI.Services.Profiles
{
    public class ParcelProfile : Profile
    {
        public ParcelProfile()
        {
            CreateMap<Parcel, ParcelDTO>();
            CreateMap<ParcelDTO, Parcel>();
        }
    }
}
