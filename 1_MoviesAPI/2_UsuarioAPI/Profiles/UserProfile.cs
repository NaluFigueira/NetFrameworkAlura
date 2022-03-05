using System;
using _2_UserAPI.Data.DTOs;
using _2_UserAPI.Models;
using AutoMapper;

namespace _2_UserAPI.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDTO, User>();
        }
    }
}
