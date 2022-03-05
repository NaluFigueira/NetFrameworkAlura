using System;
using _2_UserAPI.Data.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace _2_UserAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateUser(CreateUserDTO createUserDTO)
        {
            return Ok();
        }
    }
}
