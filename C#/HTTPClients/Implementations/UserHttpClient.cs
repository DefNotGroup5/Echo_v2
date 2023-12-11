using System.Net.Http.Json;
using System.Text.Json;
using System.Security.Claims;
using System.Text;
using Domain.Account.DTOs;
using Domain.Account.Models;
using Domain.Shopping.Models;
using HTTPClients.ClientInterfaces;

namespace HTTPClients.Implementations;

public class UserHttpClient : IUserService
{
    private readonly HttpClient _client;
    private ShoppingCart? _shoppingCart;
    public static string? Jwt { get; private set; } = "";

    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; } = principal => { };
    public event Action<ShoppingCart?>? OnShoppingCartChanged;
    public UserHttpClient(HttpClient client)
    {
        _shoppingCart = new ShoppingCart();
        _client = client;
    }
    
    public Task<ShoppingCart?> GetShoppingCart()
    {
        return Task.FromResult(_shoppingCart);
    }


    private void NotifyShoppingCartChanged()
    {
        OnShoppingCartChanged?.Invoke(_shoppingCart);
        Console.WriteLine("Shopping cart changed event fired!");
        Console.WriteLine(_shoppingCart.ItemsInCart.Count);
    }
    
    
    
    public async Task<User> CreateAsync(UserCreationDto dto)
    {
        HttpResponseMessage response = await _client.PostAsJsonAsync("http://localhost:5105/Users", dto);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        User user = JsonSerializer.Deserialize<User>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return user;
    }
    
    public async Task LoginAsync(UserLoginDto dto)
    {
        string userAsJson = JsonSerializer.Serialize(dto);
        StringContent content = new(userAsJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PatchAsync("http://localhost:5105/Users/Login", content);
        string responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }

        string token = responseContent;
        Jwt = token;
        ClaimsPrincipal principal = CreateClaimsPrincipal();
        OnAuthStateChanged.Invoke(principal);
        NotifyShoppingCartChanged();
    }

    public Task LogoutAsync()
    {
        Jwt = "";
        ClaimsPrincipal emptyPrincipal = new ClaimsPrincipal();
        OnAuthStateChanged?.Invoke(emptyPrincipal);
        return Task.CompletedTask;
    }
    
    public Task AddItemToShoppingCart(Item item, int quantity)
    {
        if (string.IsNullOrEmpty(Jwt))
        {
            Console.WriteLine("User is not logged in. Cannot add items to the shopping cart.");
            return Task.CompletedTask;
        }
        if (_shoppingCart == null) return Task.CompletedTask;
        for (int i = 0; i < quantity; i++)
        {
            _shoppingCart?.ItemsInCart.Add(item);
        }
        NotifyShoppingCartChanged();
        Console.WriteLine(_shoppingCart?.ItemsInCart.Count);
        return Task.CompletedTask;
    }
    
    public Task RemoveItemFromShoppingCart(int id)
    {
        if (_shoppingCart?.ItemsInCart != null)
            foreach (var item in _shoppingCart.ItemsInCart)
            {
                if (item.Id == id)
                {
                    _shoppingCart.ItemsInCart.Remove(item);
                    NotifyShoppingCartChanged();
                    break;
                }
            }
        return Task.CompletedTask;
    }


    public Task<ClaimsPrincipal> GetAuthAsync()
    {
        ClaimsPrincipal principal = CreateClaimsPrincipal();
        return Task.FromResult(principal);    }
    
    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        string payload = jwt.Split('.')[1];
        byte[] jsonBytes = ParseBase64WithoutPadding(payload);
        Dictionary<string, object>? keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        return keyValuePairs!.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()!));
    }
    
    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2:
                base64 += "==";
                break;
            case 3:
                base64 += "=";
                break;
        }

        return Convert.FromBase64String(base64);
    }
    
    private static ClaimsPrincipal CreateClaimsPrincipal()
    {
        if (string.IsNullOrEmpty(Jwt))
        {
            return new ClaimsPrincipal();
        }

        IEnumerable<Claim> claims = ParseClaimsFromJwt(Jwt);
    
        ClaimsIdentity identity = new(claims, "jwt");

        ClaimsPrincipal principal = new(identity);
        return principal;
    }
}