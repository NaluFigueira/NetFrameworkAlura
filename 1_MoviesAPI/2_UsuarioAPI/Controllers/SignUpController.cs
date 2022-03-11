using System;
using _2_UserAPI.Data.DTOs;
using _2_UsuarioAPI.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace _2_UserAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SignUpController : ControllerBase
    {
        private SignUpService _service;

        public SignUpController(SignUpService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult CreateUser(CreateUserDTO createUserDTO)
        {
            Result result = _service.CreateUser(createUserDTO);

            if (result.IsSuccess) return Ok(result.Successes[0]);

            return BadRequest(result.Errors[0]);
        }
    }
}
