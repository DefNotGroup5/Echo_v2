using System.Security.Claims;
using HTTPClients.ClientInterfaces;
using Microsoft.AspNetCore.Components.Authorization;

namespace PresentationView.Auth;

public class AuthProvider : AuthenticationStateProvider
{
    private readonly IUserService _userService;

    public AuthProvider(IUserService userService)
    {
        _userService = userService;
        _userService.OnAuthStateChanged += AuthStateChanged;
    }
    
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        ClaimsPrincipal principal = await _userService.GetAuthAsync();
         // bool isSeller = DetermineIfUserIsSeller(principal);
                // if (isSeller)
                // {
                //     if (principal.Identity != null)
                //         ((ClaimsIdentity)principal.Identity).AddClaim(new Claim("IsSeller", "True"));
                // }
       
        
        return new AuthenticationState(principal);
    }
    
    private bool DetermineIfUserIsSeller(ClaimsPrincipal principal)
    {
        // Get the "IsSeller" claim from the user's identity
        Claim? isSellerClaim = principal.FindFirst("IsSeller");

        // Check if the claim exists and log its value
        if (isSellerClaim != null)
        {
            Console.WriteLine($"IsSeller claim found. Value: {isSellerClaim.Value}");

            // Check if its value is equivalent to "True" (case-insensitive) and log the result
            bool isSeller = string.Equals(isSellerClaim.Value, "True", StringComparison.OrdinalIgnoreCase);
            Console.WriteLine($"IsSeller: {isSeller}");

            return isSeller;
        }
        else
        {
            Console.WriteLine("IsSeller claim not found.");
            return false;
        }
    }

    
    private void AuthStateChanged(ClaimsPrincipal principal)
    {
        NotifyAuthenticationStateChanged(
            Task.FromResult(
                new AuthenticationState(principal)
            )
        );
    }
}