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
        HttpResponseMessage response = await _client.GetAsync($"http://localhost:5111/Wishlist/by-user/{id}");
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

    public async Task RemoveWishlist(int id)
    {
        try
        {
            HttpResponseMessage response = await _client.DeleteAsync($"http://localhost:5111/Wishlist/{id}");
            if (!response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                throw new Exception(result);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}