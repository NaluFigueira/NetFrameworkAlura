using System;
using _2_UsuarioAPI.Data.Requests;
using FluentResults;
using Microsoft.AspNetCore.Identity;

namespace _2_UsuarioAPI.Services
{
    public class SignInService
    {
        private SignInManager<IdentityUser<int>> _signInManager;

        public SignInService(SignInManager<IdentityUser<int>> signInManager)
        {
            _signInManager = signInManager;
        }

        public Result SignIn(SignInRequest signInRequest)
        {
            var identityResult = _signInManager
                .PasswordSignInAsync(signInRequest.Username, signInRequest.Password, false, false);

            if (identityResult.Result.Succeeded) return Result.Ok();

            return Result.Fail("Wasn't possibile to sign in as this user");
        }
    }
}
