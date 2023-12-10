using Domain.Account.DTOs;
using Domain.Account.Models;

namespace Application.Shopping.LogicInterfaces;

public interface IOrderLogic
{
    Task<Order> CreateOrder(OrderCreationDto dto);
    Task<Order> GetOrderById(int id);
    Task<List<Order?>> GetOrders();
}