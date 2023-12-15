using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Shopping.DTOs;
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

    public async Task<Review> AddReviewAsync(ReviewCreationDto dto)
    {
        HttpResponseMessage response = await _client.PostAsJsonAsync($"/Reviews", dto);
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

    public async Task<ICollection<Review>> GetReviewsByItemAsync(int itemId)
    {
        HttpResponseMessage response = await _client.GetAsync($"/Reviews/by-item/{itemId}");
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        return JsonSerializer.Deserialize<ICollection<Review>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }

    public async Task<ICollection<Review>> GetReviewsByUserAsync(int userId)
    {
        HttpResponseMessage response = await _client.GetAsync($"Reviews/by-user/{userId}");
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        return JsonSerializer.Deserialize<ICollection<Review>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }
}
