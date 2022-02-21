using System;
using _1_MoviesAPI.Data.DTOs.Address;
using _1_MoviesAPI.Models;
using AutoMapper;

namespace _1_MoviesAPI.Profiles
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<Address, GetAddressDTO>();
            CreateMap<CreateAddressDTO, Address>();
            CreateMap<UpdateAddressDTO, Address>();
        }
    }
}
