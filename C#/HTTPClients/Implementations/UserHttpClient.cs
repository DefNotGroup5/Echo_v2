using System.Net.Http.Json;
using System.Text.Json;
using System.Security.Claims;
using System.Text;
using Domain.Account.DTOs;
using Domain.Account.Models;
using Domain.Shopping.DTOs;
using Domain.Shopping.Models;
using HTTPClients.ClientInterfaces;
using HTTPClients.Implementations.Converter;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace HTTPClients.Implementations;

public class UserHttpClient : IUserService
{
    private readonly HttpClient _client;
    public static string? Jwt { get; private set; } = "";

    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; } = principal => { };
    public UserHttpClient(HttpClient client)
    {
        _client = client;
    }
    
    
    
    
    public async Task<User> CreateAsync(UserCreationDto dto)
    {
        HttpResponseMessage response = await _client.PostAsJsonAsync("http://localhost:5105/Users/Register", dto);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        User user = JsonConvert.DeserializeObject<User>(result, new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All
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
    }

    public Task LogoutAsync()
    {
        Jwt = "";
        ClaimsPrincipal emptyPrincipal = new ClaimsPrincipal();
        OnAuthStateChanged?.Invoke(emptyPrincipal);
        return Task.CompletedTask;
    }

    public async Task<ICollection<User>> GetAllAsync()
    {
        HttpResponseMessage response = await _client.GetAsync($"http://localhost:5105/Users");
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
        
        ICollection<UserTransferDto> dtos = JsonConvert.DeserializeObject<ICollection<UserTransferDto>>(result, new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.Auto,
        })!;
        ICollection<User> users = new List<User>();
        foreach (var dto in dtos)
        {
            User user = null;
            if (dto.IsSeller)
            {
                Seller seller = new Seller(dto.User.Email, dto.User.Password)
                {
                    Id = dto.User.Id,
                    FirstName = dto.User.FirstName,
                    LastName = dto.User.LastName,
                    Password = dto.User.Password,
                    Address = dto.User.Address,
                    City = dto.User.City,
                    PostalCode = dto.User.PostalCode,
                    Country = dto.User.Country,
                };
                if (dto.IsAuthorized)
                {
                    seller.IsAuthorized = true;
                }
                user = seller;
            }
            if (dto.IsAdmin)
            {
                user = new Admin(dto.User.Email, dto.User.Password)
                {
                    Id = dto.User.Id,
                    FirstName = dto.User.FirstName,
                    LastName = dto.User.LastName,
                    Password = dto.User.Password,
                    Address = dto.User.Address,
                    City = dto.User.City,
                    PostalCode = dto.User.PostalCode,
                    Country = dto.User.Country
                };
            }
            if(dto is { IsAdmin: false, IsSeller: false })
            {
                user = new Customer(dto.User.Email, dto.User.Password)
                {
                    Id = dto.User.Id,
                    FirstName = dto.User.FirstName,
                    LastName = dto.User.LastName,
                    Password = dto.User.Password,
                    Address = dto.User.Address,
                    City = dto.User.City,
                    PostalCode = dto.User.PostalCode,
                    Country = dto.User.Country,
                };
            }

            if (user != null) users.Add(user);
        }
        return users;
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        HttpResponseMessage response = await _client.GetAsync($"http://localhost:5105/Users/{id}");
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
        UserTransferDto dto = JsonConvert.DeserializeObject<UserTransferDto>(result, new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All
        })!;
        User user = null;
        if (dto.IsSeller)
        {
            Seller seller = new Seller(dto.User.Email, dto.User.Password)
            {
                Id = dto.User.Id,
                FirstName = dto.User.FirstName,
                LastName = dto.User.LastName,
                Password = dto.User.Password,
                Address = dto.User.Address,
                City = dto.User.City,
                PostalCode = dto.User.PostalCode,
                Country = dto.User.Country,
            };
            if (dto.IsAuthorized)
            {
                seller.IsAuthorized = true;
            }
            user = seller;
        }
        if (dto.IsAdmin)
        {
            user = new Admin(dto.User.Email, dto.User.Password)
            {
                Id = dto.User.Id,
                FirstName = dto.User.FirstName,
                LastName = dto.User.LastName,
                Password = dto.User.Password,
                Address = dto.User.Address,
                City = dto.User.City,
                PostalCode = dto.User.PostalCode,
                Country = dto.User.Country
            };
        }
        if(dto is { IsAdmin: false, IsSeller: false })
        {
            user = new Customer(dto.User.Email, dto.User.Password)
            {
                Id = dto.User.Id,
                FirstName = dto.User.FirstName,
                LastName = dto.User.LastName,
                Password = dto.User.Password,
                Address = dto.User.Address,
                City = dto.User.City,
                PostalCode = dto.User.PostalCode,
                Country = dto.User.Country,
            };
        }
        return user;
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