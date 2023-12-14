using System.Net.Http.Json;
using System.Text.Json;
using Domain.Shopping.DTOs;
using Domain.Shopping.Models;
using HTTPClients.ClientInterfaces;

namespace HTTPClients.Implementations;

public class OrderHttpClient : IOrderService
{
    private readonly HttpClient _client;

    public OrderHttpClient(HttpClient client)
    {
        _client = client;
    }


    public async Task<Order?> CreateOrder(OrderCreationDto dto)
    {
        HttpResponseMessage response = await _client.PostAsJsonAsync("http://localhost:5111/Order", dto);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
        Order order = JsonSerializer.Deserialize<Order>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return order;
    }

    public async Task<Order?> GetOrderById(string orderId)
    {
        HttpResponseMessage response = await _client.GetAsync($"http://localhost:5111/Order/{orderId}");
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
        Order order = JsonSerializer.Deserialize<Order>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return order;
    }

    public async Task<ICollection<Order?>> GetOrdersBySeller(int sellerId)
    {
        HttpResponseMessage response = await _client.GetAsync($"http://localhost:5111/Order/by-seller/{sellerId}");
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
        ICollection<Order> orders = JsonSerializer.Deserialize<ICollection<Order>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return orders!;
    }

    public async Task<ICollection<Order?>> GetOrders()
    {
        HttpResponseMessage response = await _client.GetAsync($"http://localhost:5111/Order");
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
        ICollection<Order> orders = JsonSerializer.Deserialize<ICollection<Order>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return orders!;
    }
}