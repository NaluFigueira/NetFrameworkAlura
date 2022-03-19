using System;
using _2_UserAPI.Data.DTOs;
using _2_UserAPI.Models;
using _2_UsuarioAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace _2_UserAPI.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDTO, User>();
            CreateMap<User, IdentityUser<int>>();
            CreateMap<User, CustomIdentityUser>();
        }
    }
}
