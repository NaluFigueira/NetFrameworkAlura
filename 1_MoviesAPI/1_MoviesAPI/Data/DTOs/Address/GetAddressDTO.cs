using System;
using System.ComponentModel.DataAnnotations;

namespace _1_MoviesAPI.Data.DTOs.Address
{
    public class GetAddressDTO
    {
        public string Street { get; set; }

        public string Neighborhood { get; set; }

        public int Number { get; set; }
    }
}
