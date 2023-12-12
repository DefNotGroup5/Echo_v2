using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Shopping.Models;
using HTTPClients.ClientInterfaces;

namespace HTTPClients.Implementations;

public class ReviewHttpClient : IReviewService
{
    private readonly HttpClient _client;
    private readonly string _baseUrl = "http://localhost:5111"; // Adjust as necessary

    public ReviewHttpClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<Review> AddReviewAsync(Review review)
    {
        HttpResponseMessage response = await _client.PostAsJsonAsync($"{_baseUrl}/Reviews", review);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        return JsonSerializer.Deserialize<Review>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }

    public async Task<IEnumerable<Review>> GetReviewsByItemAsync(int itemId)
    {
        HttpResponseMessage response = await _client.GetAsync($"{_baseUrl}/Reviews/item/{itemId}");
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        return JsonSerializer.Deserialize<IEnumerable<Review>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }

    public async Task<IEnumerable<Review>> GetReviewsByUserAsync(int userId)
    {
        HttpResponseMessage response = await _client.GetAsync($"{_baseUrl}/Reviews/user/{userId}");
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        return JsonSerializer.Deserialize<IEnumerable<Review>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }
}
