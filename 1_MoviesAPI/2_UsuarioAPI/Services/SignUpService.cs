using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using _2_UserAPI.Data.DTOs;
using _2_UserAPI.Models;
using _2_UsuarioAPI.Data;
using _2_UsuarioAPI.Data.Requests;
using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;

namespace _2_UsuarioAPI.Services
{
    public class SignUpService
    {
        private UserManager<IdentityUser<int>> _userManager;
        private IMapper _mapper;
        private EmailService _emailService;
        private RoleManager<IdentityRole<int>> _roleManager;

        public SignUpService(UserManager<IdentityUser<int>> userManager, IMapper mapper, EmailService emailService, RoleManager<IdentityRole<int>> roleManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _emailService = emailService;
            _roleManager = roleManager;
        }

        public Result CreateUser(CreateUserDTO createUserDTO)
        {
            User user = _mapper.Map<User>(createUserDTO);
            IdentityUser<int> identityUser = _mapper.Map<IdentityUser<int>>(user);

            Task<IdentityResult> identityResult =
                _userManager.CreateAsync(identityUser, createUserDTO.Password);

            _userManager.AddToRoleAsync(identityUser, "regular");

            if(identityResult.Result.Succeeded)
            {
                var code = _userManager.GenerateEmailConfirmationTokenAsync(identityUser).Result;

                var encodedCode = HttpUtility.UrlEncode(code);

                _emailService.SendConfirmationEmail(
                    new[] { identityUser.Email },
                    "Link de Ativação",
                    identityUser.Id,
                    encodedCode
                );

                return Result.Ok().WithSuccess($"Código de ativação: {code}");
            }

            return Result.Fail("There was an error when creating the user, check input parameters and try again.");
        }

        public Result ActivateUserAccount(ActivateAccountRequest request)
        {
            var identityUser = _userManager
                .Users
                .FirstOrDefault(user => user.Id == request.Id);
            var identityResult = _userManager
                .ConfirmEmailAsync(identityUser, request.ActivationCode).Result;
            if(identityResult.Succeeded)
            {
                return Result.Ok().WithSuccess("Account activated.");
            }

            return Result.Fail("An error occured while activating account");
        }
    }
}
