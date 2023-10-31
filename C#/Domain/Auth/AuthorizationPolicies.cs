using Microsoft.Extensions.DependencyInjection;


namespace Domain.Auth

{    
    public class AuthorizationPolicies
    {
        public static void AddPolicies(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("MustBeLoggedIn", policy =>
                    policy.RequireAuthenticatedUser()
                        .RequireClaim("IsLoggedIn", "true"));
            });
        }
    }
}