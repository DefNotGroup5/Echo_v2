using System.Net.Http.Json;
using System.Text.Json;
using Domain.Account.DTOs;
using Domain.Account.Models;
using HTTPClients.ClientInterfaces;

namespace HTTPClients.Implementations;

public class CategoryHttpClient : ICategoryService
{
    
    private readonly HttpClient _client;

    public CategoryHttpClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<string?> AddCategoryAsync(CategoryCreationDto categoryCreationDto)
    {
        HttpResponseMessage response = await _client.PostAsJsonAsync("/Categories", categoryCreationDto);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
        
        Category category = JsonSerializer.Deserialize<Category>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return null;
    }

    public async Task<string?> GetCategoryByName(string categoryName)
    {
        HttpResponseMessage response = await _client.GetAsync($"/Categories/{categoryName}");
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
        return null;
    }

    public async Task<ICollection<string?>> GetAllCategories()
    {
        HttpResponseMessage response = await _client.GetAsync($"/Categories");
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
        return new List<string?>();
    }
}