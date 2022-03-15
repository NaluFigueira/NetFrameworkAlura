using System;
using System.ComponentModel.DataAnnotations;

namespace _2_UsuarioAPI.Data.Requests
{
    public class ActivateAccountRequest
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string ActivationCode { get; set; }
    }
}
