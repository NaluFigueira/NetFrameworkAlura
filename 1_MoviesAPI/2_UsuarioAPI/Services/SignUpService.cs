using System;
using System.Threading.Tasks;
using _2_UserAPI.Data.DTOs;
using _2_UserAPI.Models;
using _2_UsuarioAPI.Data;
using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;

namespace _2_UsuarioAPI.Services
{
    public class SignUpService
    {
        private UserManager<IdentityUser<int>> _userManager;
        private IMapper _mapper;

        public SignUpService(UserManager<IdentityUser<int>> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public Result CreateUser(CreateUserDTO createUserDTO)
        {
            User user = _mapper.Map<User>(createUserDTO);
            IdentityUser<int> identityUser = _mapper.Map<IdentityUser<int>>(user);

            Task<IdentityResult> identityResult =
                _userManager.CreateAsync(identityUser, createUserDTO.Password);

            if(identityResult.Result.Succeeded)
            {
                return Result.Ok().WithSuccess("User was created.");
            }

            return Result.Fail("There was an error when creating the user, check input parameters and try again.");
        }
    }
}
