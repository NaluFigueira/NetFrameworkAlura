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
                Token token = _tokenService
                    .CreateToken(
                        identityUser,
                        _signInManager.UserManager.GetRolesAsync(identityUser).Result.FirstOrDefault()
                    );
                return Result.Ok().WithSuccess(token.Value);
            }
            if(identityResult.Result.IsNotAllowed)
            {
                return Result.Fail("Needs e-mail confirmation to sign in");
            }
            return Result.Fail("Invalid username and password.");
        }

        public Result PasswordReset(PasswordResetRequest request)
        {
            IdentityUser<int> identityUser = GetIdentityUserByEmail(request.Email);

            if (identityUser != null)
            {
                IdentityResult identityResult = _signInManager
                .UserManager
                .ResetPasswordAsync(identityUser, request.Token, request.Password)
                .Result;

                if (identityResult.Succeeded)
                {
                    return Result.Ok().WithSuccess("Password reset successful!");
                }
            }

            return Result.Fail("Invalid user e-mail or invalid token");

        }

        public Result GeneratePasswordResetCode(GeneratePasswordResetCodeRequest request)
        {
            IdentityUser<int> identityUser = GetIdentityUserByEmail(request.Email);

            if (identityUser != null)
            {
                string resetCode = _signInManager
                    .UserManager.GeneratePasswordResetTokenAsync(identityUser).Result;

                return Result.Ok().WithSuccess(resetCode);
            }

            return Result.Fail("Invalid user e-mail");
        }

        private IdentityUser<int> GetIdentityUserByEmail(string email)
        {
            return _signInManager
                            .UserManager
                            .Users
                            .FirstOrDefault(user => user.NormalizedEmail == email.ToUpper());
        }

    }
}
