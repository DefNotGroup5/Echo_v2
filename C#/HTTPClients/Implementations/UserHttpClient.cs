using System.Net.Http.Json;
using System.Text.Json;
using System.Security.Claims;
using System.Text;
using Domain.Account.DTOs;
using Domain.Account.Models;
using HTTPClients.ClientInterfaces;

namespace HTTPClients.Implementations;

public class UserHttpClient : IUserService
{
    private readonly HttpClient _client;
    public static string? Jwt { get; private set; } = "";
    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; } = null!;
    
    public UserHttpClient(HttpClient client)
    {
        _client = client;
    }
    
    public async Task<User> CreateAsync(UserCreationDto dto)
    {
        HttpResponseMessage response = await _client.PostAsJsonAsync("/Users", dto);
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
    
     //public async Task<IEnumerable<User>> GetUsersAsync(string? usernameContains = null)
     //{
    //     string uri = "/Users";
    //     if (!string.IsNullOrEmpty(usernameContains))
    //     {
    //         uri += $"?username={usernameContains}";
    //     }
    //     HttpResponseMessage response = await _client.GetAsync(uri);
    //     string result = await response.Content.ReadAsStringAsync();
    //     if (!response.IsSuccessStatusCode)
    //     {
    //         throw new Exception(result);
    //     }
    //
    //     IEnumerable<User> users = JsonSerializer.Deserialize<IEnumerable<User>>(result, new JsonSerializerOptions
    //     {
    //         PropertyNameCaseInsensitive = true
    //     })!;
    //     return users;
    //throw new NotImplementedException();
     //}
    
    public async Task LoginAsync(string email, string password)
    {
        UserLoginDto userLoginDto = new(email,password)
        {
            Email = email,
            Password = password
        };

        string userAsJson = JsonSerializer.Serialize(userLoginDto);
        StringContent content = new(userAsJson, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _client.PatchAsync("/Users/Login", content);
        string responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }

        string token = responseContent;
        Jwt = token;

        //ClaimsPrincipal principal = CreateClaimsPrincipal();

        //OnAuthStateChanged.Invoke(principal);
    }

    public Task LogoutAsync()
    {
        throw new NotImplementedException();
    }

    // public async Task LogoutAsync()
    // {
    //     ClaimsPrincipal principal = CreateClaimsPrincipal();
    //     Claim? usernameClaim = principal.FindFirst(ClaimTypes.Name);
    //     
    //     string username = usernameClaim?.Value ?? "";
    //     
    //     UserLoginDto dto = new UserLoginDto(username);
    //     string dtoAsJson = JsonSerializer.Serialize(dto);
    //     StringContent body = new StringContent(dtoAsJson, Encoding.UTF8, "application/json");
    //     HttpResponseMessage response = await _client.PatchAsync("/Users/Logout", body);
    //
    //     if (!response.IsSuccessStatusCode)
    //     {
    //         string result = await response.Content.ReadAsStringAsync();
    //         throw new Exception(result);
    //     }
    //
    //     Jwt = null;
    //     principal = new ClaimsPrincipal(); // Create an empty ClaimsPrincipal
    //     OnAuthStateChanged.Invoke(principal);
    // }
    
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
    
    // public async Task RegisterAsync(User user)
    // {
    //     string userAsJson = JsonSerializer.Serialize(user);
    //     StringContent content = new(userAsJson, Encoding.UTF8, "application/json");
    //     HttpResponseMessage response = await _client.PostAsync("https://localhost:7130/auth/register", content);
    //     string responseContent = await response.Content.ReadAsStringAsync();
    //
    //     if (!response.IsSuccessStatusCode)
    //     {
    //         throw new Exception(responseContent);
    //     }
    // }
    
}