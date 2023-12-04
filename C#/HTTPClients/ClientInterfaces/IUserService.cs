using System.Security.Claims;
using Domain.Account.DTOs;
using Domain.Account.Models;

namespace HTTPClients.ClientInterfaces;

public interface IUserService
{
    public Task<User> CreateAsync(UserCreationDto dto);
    //Task<IEnumerable<User>> GetUsersAsync(string? usernameContains = null); //idk if we rly need this one rn
    public Task LoginAsync(UserLoginDto dto);
    public Task LogoutAsync();
    public Task<ClaimsPrincipal> GetAuthAsync();
    public Task AddItemToShoppingCart(Item item);

    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
    public event Action<ShoppingCart?>? OnShoppingCartChanged;
    public Task<ShoppingCart?> GetShoppingCart();
}