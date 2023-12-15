using Domain.Shopping.DTOs;
using Domain.Shopping.Models;

namespace Application.Shopping.LogicInterfaces;

public interface IOrderLogic
{
    Task<Order?> CreateOrder(OrderCreationDto dto);
    Task<Order?> GetOrderById(String orderId);
    Task<ICollection<Order?>> GetOrdersBySeller(int sellerId);
    Task<ICollection<Order?>> GetOrders();
}