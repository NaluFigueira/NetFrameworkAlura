using System;
using System.Linq;
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
            CreateMap<UpdateManagerDTO, Manager>();
            CreateMap<Manager, GetManagerDTO>()
                .ForMember(manager => manager.Cinemas, opts => opts
                .MapFrom(manager => manager.Cinemas.Select
                (cinema => new { cinema.Id, cinema.Name, cinema.Address })));
        }
    }
}
