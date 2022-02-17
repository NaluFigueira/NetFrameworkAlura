using System;
using _1_MoviesAPI.Data.DTOs;
using AutoMapper;
using MoviesAPI.Models;

namespace _1_MoviesAPI.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<CreateMovieDTO, Movie>();
            CreateMap<Movie, GetMovieDTO>();
            CreateMap<UpdateMovieDTO, Movie>();
        }
    }
}
