using System.Net.Http.Json;
using System.Text.Json;
using Domain.Shopping.DTOs;
using Domain.Shopping.Models;
using HTTPClients.ClientInterfaces;

namespace HTTPClients.Implementations;

public class WishlistHttpClient : IWishlistService
{
    private readonly HttpClient _client;

    public WishlistHttpClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<Wishlist?> CreateAsync(WishlistCreationDto wishlistCreationDto)
    {
        HttpResponseMessage response = await _client.PostAsJsonAsync("http://localhost:5111/Wishlist", wishlistCreationDto);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        Wishlist wishlist = JsonSerializer.Deserialize<Wishlist>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return wishlist;
    }

    public async Task<ICollection<Wishlist?>?> GetByUserIdAsync(int id)
    {
        HttpResponseMessage response = await _client.GetAsync($"http://localhost:5111/Wishlist/{id}");
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        ICollection<Wishlist?>? wishlistItems = JsonSerializer.Deserialize<ICollection<Wishlist>>(result,
            new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            })!;
        return wishlistItems;
    }
}