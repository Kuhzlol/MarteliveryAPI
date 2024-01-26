using AutoMapper;
using MarteliveryAPI.Models.Domain;
using MarteliveryAPI.Models.DTOs.Admin;
using MarteliveryAPI.Models.DTOs.Customer;
using MarteliveryAPI.Models.DTOs.User;

namespace MarteliveryAPI.Profiles
{
    public class CarrierRatingProfile : Profile
    {
        public CarrierRatingProfile()
        {
            CreateMap<CarrierRating, GetCarrierRatingInfoDTO>().ReverseMap();
            CreateMap<AdminCarrierRatingDTO, CarrierRating>().ReverseMap();
            CreateMap<CustomerCarrierRatingDTO, CarrierRating>().ReverseMap();
        }
    }
}
