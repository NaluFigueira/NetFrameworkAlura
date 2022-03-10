using System;
using System.ComponentModel.DataAnnotations;

namespace _2_UsuarioAPI.Data.Requests
{
    public class SignInRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
