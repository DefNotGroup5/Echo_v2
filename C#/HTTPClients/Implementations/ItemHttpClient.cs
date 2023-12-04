using System.Net.Http.Json;
using HTTPClients.ClientInterfaces;

namespace HTTPClients.Implementations;

public class ItemHttpClient : IItemService
{ 
    private readonly HttpClient _client;

    public ItemHttpClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<string?> CreateAsync(string itemCreationDto)
    {
        HttpResponseMessage response = await _client.PostAsJsonAsync("/Items", itemCreationDto);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
        /*
        Item item = JsonSerializer.Deserialize<Item>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        */
        return null;
    }

    public async Task<string?> GetById(int id)
    {
        HttpResponseMessage response = await _client.GetAsync($"/Items/{id}");
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
        /*
        Item item = JsonSerializer.Deserialize<Item>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        */
        return null;
    }

    public async Task<ICollection<string?>> GetAsync()
    {
        HttpResponseMessage response = await _client.GetAsync($"/Items");
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
        /*
        Item item = JsonSerializer.Deserialize<Item>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        */
        return new List<string?>();
    }
}