﻿using AutoMapper;
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
            CreateMap<AdminParcelCreateDTO, Parcel>().ReverseMap();
            CreateMap<AdminParcelUpdateDTO, Parcel>().ReverseMap();

            CreateMap<Parcel, CustomerParcelDTO>().ReverseMap();
            CreateMap<CustomerParcelDTO, Parcel>().ReverseMap();
        }
    }
}
