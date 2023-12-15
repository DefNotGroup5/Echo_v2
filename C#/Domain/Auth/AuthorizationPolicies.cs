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
                    context.User.HasClaim(c => c is { Type: "IsAdmin", Value: "True" })));
            options.AddPolicy("IsSeller", policy =>
                policy.RequireAssertion(context =>
                    context.User.HasClaim(c => c is { Type: "IsSeller", Value: "True" })
                    && context.User.HasClaim(c => c is { Type: "IsAuthorizedSeller", Value: "False" })));
            options.AddPolicy("IsAuthorizedSeller", policy =>
                policy.RequireAssertion(context =>
                    context.User.HasClaim(c => c is { Type: "IsAuthorizedSeller", Value: "True" })
                    &&
                    context.User.HasClaim(c => c is { Type: "IsSeller", Value: "True" })));
            options.AddPolicy("IsCustomer", policy =>
                policy.RequireAssertion(context =>
                    context.User.HasClaim(c => c is { Type: "IsCustomer", Value: "True" })));

            
            //options.AddPolicy("isItemNull", policy=>
                
        });
    }
    
   
}
