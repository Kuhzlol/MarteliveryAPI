using AutoMapper;
using MarteliveryAPI.Models.Domain;
using MarteliveryAPI.Models.DTOs.Admin;
using MarteliveryAPI.Models.DTOs.Customer;

namespace MarteliveryAPI.Services.Profiles
{
    public class ParcelProfile : Profile
    {
        public ParcelProfile()
        {
            CreateMap<Parcel, AdminParcelDTO>().ReverseMap();
            CreateMap<AdminCreateParcelDTO, Parcel>().ReverseMap();
            CreateMap<AdminUpdateParcelDTO, Parcel>().ReverseMap();

            CreateMap<Parcel, CustomerParcelDTO>().ReverseMap();
            CreateMap<CustomerParcelDTO, Parcel>().ReverseMap();
        }
    }
}
