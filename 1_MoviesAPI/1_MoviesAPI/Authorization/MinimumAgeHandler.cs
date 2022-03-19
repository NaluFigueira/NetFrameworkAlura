using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace _1_MoviesAPI.Authorization
{
    public class MinimumAgeHandler : AuthorizationHandler<MinimumAgeRequirements>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirements requirement)
        {
            if(!context.User.HasClaim(context => context.Type == ClaimTypes.DateOfBirth))
            {
                return Task.CompletedTask;
            }

            var birthDate = Convert.ToDateTime(context.User.FindFirst(claim =>
                claim.Type == ClaimTypes.DateOfBirth
            ).Value);

            int age = DateTime.Today.Year - birthDate.Year;

            if(birthDate > DateTime.Today.AddYears(-age))
            {
                age--;
            }

            if (age >= requirement.MinimumAge) context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
