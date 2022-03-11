using System;
using FluentResults;
using Microsoft.AspNetCore.Identity;

namespace _2_UsuarioAPI.Services
{
    public class SignOutService
    {
        private SignInManager<IdentityUser<int>> _signInManager;

        public SignOutService(SignInManager<IdentityUser<int>> signInManager)
        {
            _signInManager = signInManager;
        }

        public Result SignOut()
        {
            var resultIdentity = _signInManager.SignOutAsync();

            if (resultIdentity.IsCompletedSuccessfully)
            {
                return Result.Ok().WithSuccess("User signed out.");
            }

            return Result.Fail("There was a problem while signing out, try again later.");
        }
    }
}
