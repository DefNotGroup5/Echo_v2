using System.Security.Claims;
using Domain.Account.DTOs;
using Domain.Account.Models;

namespace HTTPClients.ClientInterfaces;

public interface IUserService
{
    public Task<User> CreateAsync(UserCreationDto dto);
    Task<IEnumerable<User>> GetUsersAsync(string? usernameContains = null); //idk if we rly need this one rn
    public Task LoginAsync(string username, string password);
    public Task LogoutAsync();
    public Task<ClaimsPrincipal> GetAuthAsync();

    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
    public Task RegisterAsync(User user); //from codelabs not sure about it
}