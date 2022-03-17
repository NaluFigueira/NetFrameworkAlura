using System;
using _2_UsuarioAPI.Data.Requests;
using _2_UsuarioAPI.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace _2_UsuarioAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SignInController : ControllerBase
    {
        private SignInService _signInService;

        public SignInController(SignInService signInService)
        {
            _signInService = signInService;
        }

        [HttpPost]
        public IActionResult SignIn(SignInRequest signInRequest)
        {
            Result result = _signInService.SignIn(signInRequest);

            if (result.IsSuccess)
            {
                return Ok(result.Successes[0]);
            }

            return Unauthorized(result.Errors[0]);
        }

        [HttpPost("/generate-password-reset-code")]
        public IActionResult GeneratePasswordResetCode(GeneratePasswordResetCodeRequest request)
        {
            Result result = _signInService.GeneratePasswordResetCode(request);

            if (result.IsSuccess)
            {
                return Ok(result.Successes[0]);
            }

            return Unauthorized(result.Errors[0]);
        }

        [HttpPost("/password-reset")]
        public IActionResult PasswordReset(PasswordResetRequest request)
        {
            Result result = _signInService.PasswordReset(request);

            if (result.IsSuccess)
            {
                return Ok(result.Successes[0]);
            }

            return Unauthorized(result.Errors[0]);
        }

    }
}
