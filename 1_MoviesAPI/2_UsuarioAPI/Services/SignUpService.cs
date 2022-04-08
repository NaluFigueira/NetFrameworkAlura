using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using _2_UserAPI.Data.DTOs;
using _2_UserAPI.Models;
using _2_UsuarioAPI.Data;
using _2_UsuarioAPI.Data.Requests;
using _2_UsuarioAPI.Models;
using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;

namespace _2_UsuarioAPI.Services
{
    public class SignUpService
    {
        private UserManager<CustomIdentityUser> _userManager;
        private IMapper _mapper;
        private EmailService _emailService;

        public SignUpService(UserManager<CustomIdentityUser> userManager, IMapper mapper, EmailService emailService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _emailService = emailService;
        }

        public Result CreateUser(CreateUserDTO createUserDTO)
        {
            User user = _mapper.Map<User>(createUserDTO);
            CustomIdentityUser identityUser = _mapper.Map<CustomIdentityUser>(user);

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
