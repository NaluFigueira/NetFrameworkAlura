using System;
using _1_MoviesAPI.Data.DTOs;
using _1_MoviesAPI.Models;
using AutoMapper;

namespace _1_MoviesAPI.Profiles
{
    public class SessionProfile : Profile
    {
        public SessionProfile()
        {
            CreateMap<Session, GetSessionDTO>();
            CreateMap<CreateSessionDTO, Session>();
            CreateMap<UpdateSessionDTO, Session>();
        }
    }
}
