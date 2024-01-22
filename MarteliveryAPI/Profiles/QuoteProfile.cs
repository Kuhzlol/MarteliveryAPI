using AutoMapper;
using MarteliveryAPI.Models.Domain;
using MarteliveryAPI.Models.DTOs.Admin;
using MarteliveryAPI.Models.DTOs.Carrier;

namespace MarteliveryAPI.Profiles
{
    public class QuoteProfile : Profile
    {
        public QuoteProfile()
        {
            CreateMap<Quote, AdminQuoteInfoDTO>().ReverseMap();
            CreateMap<AdminQuoteCreateDTO, Quote>().ReverseMap();
            CreateMap<AdminQuoteUpdateDTO, Quote>().ReverseMap();

            CreateMap<Quote, CarrierQuoteDTO>().ReverseMap();
            CreateMap<CarrierQuoteDTO, Quote>().ReverseMap();
        }
    }
}
