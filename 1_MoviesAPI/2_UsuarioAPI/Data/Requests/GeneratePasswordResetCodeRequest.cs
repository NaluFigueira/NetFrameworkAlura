using System;
using System.ComponentModel.DataAnnotations;

namespace _2_UsuarioAPI.Data.Requests
{
    public class GeneratePasswordResetCodeRequest
    {
        [Required]
        public string Email { get; set; }
    }
}
