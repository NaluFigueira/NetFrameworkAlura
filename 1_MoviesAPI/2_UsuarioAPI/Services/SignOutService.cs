using System;
using _2_UsuarioAPI.Models;
using FluentResults;
using Microsoft.AspNetCore.Identity;

namespace _2_UsuarioAPI.Services
{
    public class SignOutService
    {
        private SignInManager<CustomIdentityUser> _signInManager;

        public SignOutService(SignInManager<CustomIdentityUser> signInManager)
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
