using System;
using Microsoft.AspNetCore.Identity;

namespace _2_UsuarioAPI.Models
{
    public class CustomIdentityUser : IdentityUser<int>
    {
        public DateTime BirthDate { get; set; }
    }
}
