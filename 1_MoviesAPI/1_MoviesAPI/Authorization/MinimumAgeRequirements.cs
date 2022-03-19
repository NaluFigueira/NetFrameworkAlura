using System;
using Microsoft.AspNetCore.Authorization;

namespace _1_MoviesAPI.Authorization
{
    public class MinimumAgeRequirements : IAuthorizationRequirement
    {
        public int MinimumAge { get; set; }

        public MinimumAgeRequirements(int _minimumAge)
        {
            MinimumAge = _minimumAge;
        }
    }
}
