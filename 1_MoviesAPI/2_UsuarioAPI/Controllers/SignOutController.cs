using System;
using _2_UsuarioAPI.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace _2_UsuarioAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SignOutController : ControllerBase
    {
        private SignOutService _service;

        public SignOutController(SignOutService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult SignOutUser()
        {
            Result result = _service.SignOut();

            if (result.IsSuccess) return Ok(result.Successes[0]);

            return Unauthorized(result.Errors[0]);
        }
    }
}
