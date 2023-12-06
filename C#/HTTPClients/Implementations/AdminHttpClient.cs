using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Domain.Account.Models;
using System.Collections.Generic;
using System;
using HTTPClients.ClientInterfaces;

namespace HTTPClients.Implementations;

public class AdminHttpClient : IAdminService
{
    private readonly HttpClient _client;

    public AdminHttpClient(HttpClient client)
    {
        _client = client;
    }
    
    public async Task<IEnumerable<User>> ListSellersAsync()
    {
        HttpResponseMessage response = await _client.GetAsync("/api/admin/sellers");
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to retrieve sellers");
        }

        string result = await response.Content.ReadAsStringAsync();
        var sellers = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<User>>(result, new System.Text.Json.JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        return sellers ?? Enumerable.Empty<User>();
    }
    
    public async Task AuthorizeSellerAsync(int userId, bool isAuthorized)
    {
        HttpResponseMessage response = await _client.PostAsJsonAsync($"/api/admin/authorize-seller/{userId}", isAuthorized);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to authorize seller");
        }
    }

    public Task DeleteSellerAsync(int id)
    {
        throw new NotImplementedException();
    }
}