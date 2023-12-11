using System.Net.Http.Json;
using System.Text.Json;
using Domain.Shopping.DTOs;
using Domain.Shopping.Models;
using HTTPClients.ClientInterfaces;

namespace HTTPClients.Implementations;

public class ItemHttpClient : IItemService
{ 
    private readonly HttpClient _client;

    public ItemHttpClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<Item?> CreateAsync(ItemCreationDto itemCreationDto)
    {
        HttpResponseMessage response = await _client.PostAsJsonAsync("/Items", itemCreationDto);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
        Item item = JsonSerializer.Deserialize<Item>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return item;
    }

    public async Task<Item?> GetById(int id)
    {
        HttpResponseMessage response = await _client.GetAsync($"/Items/{id}");
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
        Item item = JsonSerializer.Deserialize<Item>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        
        return item;
    }

    public async Task<ICollection<Item?>?> GetAsync()
    {
        HttpResponseMessage response = await _client.GetAsync($"/Items");
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        ICollection<Item?>? items = JsonSerializer.Deserialize<ICollection<Item>>(result, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        })!;
        return items;
    }
}