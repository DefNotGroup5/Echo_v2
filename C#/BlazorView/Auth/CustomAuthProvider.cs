﻿using System.Security.Claims;
using HTTPClients.ClientInterfaces;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorView.Auth
{
    public class CustomAuthProvider : AuthenticationStateProvider
    {
        private readonly IUserService _userService;

        public CustomAuthProvider(IUserService userService)
        {
            _userService = userService;
            _userService.OnAuthStateChanged += AuthStateChanged;
        }
        
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            ClaimsPrincipal principal = await _userService.GetAuthAsync();
            return new AuthenticationState(principal);
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
}