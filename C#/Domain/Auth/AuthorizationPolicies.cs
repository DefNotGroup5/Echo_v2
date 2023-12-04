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
        });
    }
}
