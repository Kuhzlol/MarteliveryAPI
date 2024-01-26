using AutoMapper;
using MarteliveryAPI.Models.Domain;
using MarteliveryAPI.Models.DTOs.Admin;
using MarteliveryAPI.Models.DTOs.Carrier;
using MarteliveryAPI.Models.DTOs.User;

namespace MarteliveryAPI.Profiles
{
    public class DeliveryProfile : Profile
    {
        public DeliveryProfile()
        {
            CreateMap<Delivery, DeliveryInfoDTO>().ReverseMap();
            CreateMap<AdminCreateDeliveryDTO, Delivery>().ReverseMap();
            CreateMap<AdminUpdateDeliveryDTO, Delivery>().ReverseMap();

            CreateMap<CarrierDeliveryDTO, Delivery>().ReverseMap();
        }
    }
}
