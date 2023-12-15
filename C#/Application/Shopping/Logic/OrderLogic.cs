using System.Collections;
using System.Data;
using Application.Shopping.LogicInterfaces;
using Domain.Shopping.DTOs;
using Domain.Shopping.Models;
using GrpcClientServices.Services;

namespace Application.Shopping.Logic;

public class OrderLogic : IOrderLogic
{
    private readonly UsersService _usersService;
    private readonly ItemService _itemsService;
    private readonly OrderService _orderService;

    public OrderLogic(UsersService usersService, ItemService itemsService, OrderService orderService)
    {
        _orderService = orderService;
        _usersService = usersService;
        _itemsService = itemsService;
    }

    public async Task<Order?> CreateOrder(OrderCreationDto dto)
    {
        try
        {
            string validation = await ValidateCreationDto(dto);
            if (!string.IsNullOrEmpty(validation))
            {
                throw new Exception(validation);
            }

            return await _orderService.CreateOrderAsync(dto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Order?> GetOrderById(String orderId)
    {
        try
        {
            ICollection<Order> orders = await _orderService.GetAllAsync();
            foreach (var order in orders)
            {
                if (order.OrderId == orderId)
                    return order;
            }

            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<ICollection<Order?>> GetOrdersBySeller(int sellerId)
    {
        ICollection<Order> orders = await _orderService.GetAllAsync();
        ICollection<Order> ordersBySeller = new List<Order>();
        foreach (var order in orders)
        {
            ICollection<int> itemIds = new List<int>();
            double totalPrice = 0;
            foreach (var itemId in order.ItemIds)
            {
                Item? item = await _itemsService.GetItemByIdAsync(itemId);
                if (item?.SellerId == sellerId)
                {
                    totalPrice += item.Price;
                    itemIds.Add(itemId);
                }
            }

            Order newOrder = new Order()
            {
                CustomerId = order.CustomerId,
                TotalPrice = totalPrice,
                Status = order.Status,
                ItemIds = itemIds,
                OrderDate = order.OrderDate,
                OrderId = order.OrderId
            };
            ordersBySeller.Add(newOrder);
        }
        return ordersBySeller!;
    }

    public async Task<ICollection<Order?>> GetOrders()
    {
        try
        {
            ICollection<Order?> orders = (await _orderService.GetAllAsync())!;
            return orders;
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
        User? user = await _usersService.GetByIdAsync(orderCreationDto.CustomerId);
        if (user == null)
        {
            return "User with id: " + orderCreationDto.CustomerId + " does not exist!";
        }
        if (orderCreationDto.ItemIds.Count == 0)
        {
            return "Order must contain at least one item!";
        }
        Dictionary<int, int> itemQuantityMap = new Dictionary<int, int>();
        foreach (var itemId in orderCreationDto.ItemIds)
        {
                Item? itemFromDb = await _itemsService.GetItemByIdAsync(itemId);
                if (itemFromDb == null)
                {
                    return "Item with id: " + itemId + " does not exist!";
                }
                itemQuantityMap[itemId] = itemQuantityMap.TryGetValue(itemId, out var existingQuantity)
                        ? existingQuantity + 1
                        : 1;
        }
        foreach (var (itemId, quantity) in itemQuantityMap)
        {
                Item? itemFromDb = await _itemsService.GetItemByIdAsync(itemId);
                if (quantity > itemFromDb?.Quantity)
                {
                    return $"Item with id: {itemId} has exceeded the allowed quantity!";
                }
        }
        return "";
    }
}