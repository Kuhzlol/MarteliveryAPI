using AutoMapper;
using MarteliveryAPI.Models.Domain;
using MarteliveryAPI.Models.DTOs.Admin;
using MarteliveryAPI.Models.DTOs.Customer;
using MarteliveryAPI.Models.DTOs.User;

namespace MarteliveryAPI.Profiles
{
    public class ParcelProfile : Profile
    {
        public ParcelProfile()
        {
            CreateMap<Parcel, AdminParcelInfoDTO>().ReverseMap();
            CreateMap<AdminCreateParcelDTO, Parcel>().ReverseMap();
            CreateMap<AdminUpdateParcelDTO, Parcel>().ReverseMap();

            CreateMap<Parcel, CustomerCreateParcelDTO>().ReverseMap();
            CreateMap<CustomerCreateParcelDTO, Parcel>().ReverseMap();
            CreateMap<GetParcelInfoDTO, Parcel>().ReverseMap();
        }
    }
}
