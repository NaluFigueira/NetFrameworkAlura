using System;
using System.Linq;
using _2_UsuarioAPI.Data.Requests;
using _2_UsuarioAPI.Models;
using FluentResults;
using Microsoft.AspNetCore.Identity;

namespace _2_UsuarioAPI.Services
{
    public class SignInService
    {
        private SignInManager<IdentityUser<int>> _signInManager;
        private TokenService _tokenService;

        public SignInService(SignInManager<IdentityUser<int>> signInManager, TokenService tokenService)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public Result SignIn(SignInRequest signInRequest)
        {
            var identityResult = _signInManager
                .PasswordSignInAsync(signInRequest.Username, signInRequest.Password, false, false);

            if (identityResult.Result.Succeeded)
            {
                var identityUser = _signInManager
                    .UserManager
                    .Users
                    .FirstOrDefault(user => user.NormalizedUserName == signInRequest.Username.ToUpper());
                Token token = _tokenService.CreateToken(identityUser);
                return Result.Ok().WithSuccess(token.Value);
            }

            return Result.Fail("Invalid username and password.");
        }
    }
}
