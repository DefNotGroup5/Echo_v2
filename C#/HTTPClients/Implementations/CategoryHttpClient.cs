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

    public async Task<Category?> AddCategoryAsync(CategoryCreationDto categoryCreationDto)
    {
        HttpResponseMessage response = await _client.PostAsJsonAsync($"http://localhost:5105/Category", categoryCreationDto);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
        
        Category category = JsonSerializer.Deserialize<Category>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return category;
    }

    public async Task<Category?> GetCategoryByName(string categoryName)
    {
        HttpResponseMessage response = await _client.GetAsync($"http://localhost:5105/Category/{categoryName}");
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
        Category category = JsonSerializer.Deserialize<Category>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return category;
    }

    public async Task<ICollection<Category?>> GetAllCategories()
    {
        HttpResponseMessage response = await _client.GetAsync("http://localhost:5105/Category");
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
        ICollection<Category> categories = JsonSerializer.Deserialize<ICollection<Category>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return categories;
    }
    
    public async Task DeleteCategory(string categoryName)
    {
        HttpResponseMessage response = await _client.DeleteAsync($"http://localhost:5105/Category/{categoryName}");
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
    }
    
    
    
    
}