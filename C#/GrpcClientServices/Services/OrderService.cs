using System.Collections;
using Domain.Shopping.DTOs;
using Domain.Shopping.Models;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;

namespace GrpcClientServices.Services;

public class OrderService : GrpcClientServices.OrderService.OrderServiceClient
{
    private readonly GrpcChannel _channel;
    private readonly ItemService _itemService;

    public OrderService(ItemService itemService)
    {
        _itemService = itemService;
        _channel = GrpcChannel.ForAddress("http://localhost:3030");
    }

    public async Task<Order?> CreateOrderAsync(OrderCreationDto dto)
    {
        try
        {
            ICollection<Item?> items = new List<Item?>(); //For total price
            CreateOrderRequest orderRequest = new CreateOrderRequest() //Initial Order Request
            {
                CustomerId = dto.CustomerId,
                OrderDate = Timestamp.FromDateTime(DateTime.UtcNow),
                Status = "Pending",
            }; 
            
            foreach(var itemId in dto.ItemIds)
            {
                orderRequest.ItemId.Add(itemId); //Add item ids from the dto to order request
                items.Add(await _itemService.GetItemByIdAsync(itemId)); //For total price finding items
            }

            //Figuring out total price and adding to order request
            double totalPrice = 0; 
            foreach (var item in items)
            {
                if (item != null) 
                    totalPrice += item.Price;
            }
            orderRequest.TotalPrice = totalPrice;
            
            var client = new GrpcClientServices.OrderService.OrderServiceClient(_channel);
            var reply = await client.CreateOrderAsync(orderRequest); //Getting reply from grpc

            //Generating Code from GRPC
            ICollection<int> itemIds = new List<int>(); 
            foreach (var itemId in reply.OrderItem.ItemId)
            {
                itemIds.Add(itemId);
            }
            
            //Create Order for returning
            Order order = new Order()
            {
                CustomerId = reply.OrderItem.CustomerId,
                OrderDate = reply.OrderItem.OrderDate.ToDateTime(),
                OrderId = reply.OrderItem.OrderId,
                Status = reply.OrderItem.Status,
                ItemIds = itemIds,
                TotalPrice = reply.OrderItem.TotalPrice
            }; 
            return order;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<ICollection<Order>> GetAllAsync()
    {
        try
        {
            var grpcClient = new GrpcClientServices.OrderService.OrderServiceClient(_channel);
            var reply = await grpcClient.GetAllOrdersAsync(new GetAllOrdersRequest());
            
            ICollection<Order> orders = new List<Order>();
            
            foreach (var order in reply.Orders)
            {
                ICollection<int> orderIds = new List<int>();
                foreach (var orderId in order.ItemId)
                {
                    orderIds.Add(orderId);
                }
                orders.Add(new Order()
                {
                    CustomerId = order.CustomerId,
                    OrderDate = order.OrderDate.ToDateTime(),
                    OrderId = order.OrderId,
                    Status = order.Status,
                    ItemIds = orderIds,
                    TotalPrice = order.TotalPrice
                });
            }
            return orders;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}