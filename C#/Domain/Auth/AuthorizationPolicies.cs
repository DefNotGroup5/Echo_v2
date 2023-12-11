using System.Security.Claims;
using Domain.Shopping.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Auth;

public static class AuthorizationPolicies
{
    public static void AddPolicies(IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("IsAdmin", policy =>
                policy.RequireAssertion(context =>
                    context.User.HasClaim(c => c.Type == "IsAdmin" && c.Value == "True")));
            
            options.AddPolicy("IsSeller", policy =>
                policy.RequireAssertion(context =>
                    context.User.HasClaim(c => c.Type == "IsSeller" && c.Value == "True")));

            
            //options.AddPolicy("isItemNull", policy=>
                
        });
    }
    
   
}
