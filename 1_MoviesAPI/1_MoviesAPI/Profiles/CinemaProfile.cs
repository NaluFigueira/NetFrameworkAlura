using System;
using _1_MoviesAPI.Data.DTOs.Cinema;
using _1_MoviesAPI.Models;
using AutoMapper;

namespace _1_MoviesAPI.Profiles
{
    public class CinemaProfile : Profile
    {
        public CinemaProfile()
        {
            CreateMap<Cinema, GetCinemaDTO>();
            CreateMap<CreateCinemaDTO, Cinema>();
            CreateMap<UpdateCinemaDTO, Cinema>();
        }
    }
}
