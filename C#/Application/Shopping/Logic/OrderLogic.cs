using System.Data;
using Application.Shopping.LogicInterfaces;
using Domain.Account.DTOs;
using Domain.Account.Models;
using GrpcClientServices.Services;

namespace Application.Shopping.Logic;

public class OrderLogic : IOrderLogic
{
    private readonly UsersService _usersService;
    private readonly ItemService _itemsService;

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
    
    //Add Validation
    private async Task<string> ValidateCreationDto(OrderCreationDto orderCreationDto)
    {
        string validation = "";
        User? user = await _usersService.GetByIdAsync(orderCreationDto.CustomerId);
        if (user == null)
        {
            validation += "User with id: " + orderCreationDto.CustomerId + " does not exist!";
        }
        if (orderCreationDto.ItemsInOrder.Count == 0)
        {
            validation += "Order must contain at least one item!";
        }
        else
        {
            foreach (var item in orderCreationDto.ItemsInOrder)
            {
                Item? itemFromDb = await _itemsService.GetItemByIdAsync(item.Id);
                if (itemFromDb == null)
                {
                    validation += "Item with id: " + item.Id + " does not exist!";
                }
                else
                {
                    if (itemFromDb.Quantity < item.Quantity)
                    {
                        validation += "Item with id: " + item.Id + " does not have enough quantity!";
                    }
                }
            }
        }
        return validation;
    }
}