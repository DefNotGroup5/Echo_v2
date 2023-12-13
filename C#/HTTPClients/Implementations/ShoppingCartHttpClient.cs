using System.Net.Http.Json;
using System.Text.Json;
using Domain.Shopping.DTOs;
using Domain.Shopping.Models;
using HTTPClients.ClientInterfaces;

namespace HTTPClients.Implementations;

public class ShoppingCartHttpClient : IShoppingCartService
{
    private readonly HttpClient _client;

    public ShoppingCartHttpClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<CartItem?> CreateAsync(CartItemCreationDto dto)
    {
        HttpResponseMessage response = await _client.PostAsJsonAsync("http://localhost:5111/Cart", dto);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
        CartItem item = JsonSerializer.Deserialize<CartItem>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return item;
    }

    public async Task<ICollection<CartItem>?> GetByCustomerId(int id)
    {
        HttpResponseMessage response = await _client.GetAsync($"http://localhost:5111/Cart/{id}");
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
        ICollection<CartItem> items = JsonSerializer.Deserialize<ICollection<CartItem>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return items;
    }
}