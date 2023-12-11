using Domain.Shopping.DTOs;
using Domain.Shopping.Models;

namespace Application.Shopping.LogicInterfaces;

public interface IOrderLogic
{
    Task<Order> CreateOrder(OrderCreationDto dto);
    Task<Order> GetOrderById(int id);
    Task<List<Order?>> GetOrders();
}