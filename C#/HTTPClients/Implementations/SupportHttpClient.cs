using System.Net.Http.Json;
using System.Text.Json;
using Domain.Account.DTOs;
using Domain.Shopping.Models;
using HTTPClients.ClientInterfaces;

namespace HTTPClients.Implementations;

public class SupportHttpClient : ISupportService
{
    private readonly HttpClient _client;

    public SupportHttpClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<Message?> RequestSupport(MessageRequestDto dto)
    {
        HttpResponseMessage response = await _client.PostAsJsonAsync("http://localhost:5105/Support", dto);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
        Message message = JsonSerializer.Deserialize<Message>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return message;
    }

    public async Task ProvideSupport(MessageResponseDto dto)
    {
        HttpResponseMessage response = await _client.PatchAsJsonAsync("http://localhost:5105/Support", dto);
        if (!response.IsSuccessStatusCode)
        {
            string result = await response.Content.ReadAsStringAsync();
            throw new Exception(result);
        }
    }

    public async Task<ICollection<Message>> GetAll()
    {
        HttpResponseMessage response = await _client.GetAsync("http://localhost:5105/Support");
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        ICollection<Message>? messages = JsonSerializer.Deserialize<ICollection<Message>>(result, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        })!;
        return messages;
    }

    public async Task<ICollection<Message>> GetAllByIsAnswered(bool isAnswered)
    {
        ICollection<Message> messages = await GetAll();
        ICollection<Message> messagesToReturn = new List<Message>();
        foreach (var message in messages)
        {
            if (message.Answered == isAnswered)
            {
                messagesToReturn.Add(message);
            }
        }
        return messagesToReturn;
    }
}