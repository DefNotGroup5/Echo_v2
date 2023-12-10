using System.Data;
using Application.Shopping.LogicInterfaces;
using Domain.Account.DTOs;
using Domain.Account.Models;

namespace Application.Shopping.Logic;

public class OrderLogic : IOrderLogic
{

    public Task<Order> CreateOrder(OrderCreationDto dto)
    {
        try
        {
            var order = new Order(customerId: dto.CustomerId);
            foreach (var item in dto.ItemsInOrder)
            {
                order.ItemsInOrder.Add(item);
                order.TotalPrice += item.Price;
            }

            return Task.FromResult(order);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Task<Order> GetOrderById(int id)
    {
        try
        {
            var order = new Order(customerId: 1); // wait for service :*
            return Task.FromResult(order);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Task<List<Order?>> GetOrders()
    {
        try{
            var orders = new List<Order?>();
            return Task.FromResult(orders);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}