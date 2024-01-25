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
            CreateMap<AdminParcelCreateDTO, Parcel>().ReverseMap();
            CreateMap<AdminParcelUpdateDTO, Parcel>().ReverseMap();

            CreateMap<Parcel, CustomerParcelDTO>().ReverseMap();
            CreateMap<CustomerParcelDTO, Parcel>().ReverseMap();

            CreateMap<Quote, QuoteInfoDTO>().ReverseMap();
            CreateMap<CustomerAcceptQuoteDTO, Quote>().ReverseMap();
        }
    }
}
