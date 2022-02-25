using System;
using _1_MoviesAPI.Data.DTOs;
using _1_MoviesAPI.Models;
using AutoMapper;

namespace _1_MoviesAPI.Profiles
{
    public class ManagerProfile : Profile
    {
        public ManagerProfile()
        {
            CreateMap<CreateManagerDTO, Manager>();
            CreateMap<Manager, GetManagerDTO>();
            CreateMap<UpdateManagerDTO, Manager>();
        }
    }
}
