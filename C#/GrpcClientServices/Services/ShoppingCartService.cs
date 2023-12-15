using System.Collections;
using Domain.Shopping.DTOs;
using Domain.Shopping.Models;
using Grpc.Net.Client;

namespace GrpcClientServices.Services;

public class ShoppingCartService : GrpcClientServices.ShoppingCartService.ShoppingCartServiceClient
{
    private readonly GrpcChannel _channel;
    
    public ShoppingCartService()
    {
        _channel = GrpcChannel.ForAddress("http://localhost:3030");
    }

    public async Task<CartItem?> AddToShoppingCartAsync(CartItemCreationDto dto) 
    {
        try
        {
            var client = new GrpcClientServices.ShoppingCartService.ShoppingCartServiceClient(_channel);
            var reply = await client.AddToShoppingCartAsync(new AddToShoppingCartRequest()
            {
                CustomerId = dto.CustomerId,
                ItemId = dto.ItemId,
                Quantity = dto.Quantity
            });
            GrpcCartItem cartItem = reply.CartItem;
            CartItem itemToReturn = new CartItem()
            {
                Id = cartItem.Id,
                ItemId = cartItem.ItemId,
                CustomerId = cartItem.CustomerId,
                Quantity = cartItem.Quantity
            };
            return itemToReturn;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return null;
    }

    public async Task ClearCart(int customerId)
    {
        try
        {
            var client = new GrpcClientServices.ShoppingCartService.ShoppingCartServiceClient(_channel);
            var reply = await client.ClearCartAsync(new ClearCartRequest()
            {
                CustomerId = customerId
            });
            Console.WriteLine(reply);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<ICollection<CartItem>?> GetAllCartItemsAsync()
    {
        try
        {
            var client = new GrpcClientServices.ShoppingCartService.ShoppingCartServiceClient(_channel);
            var reply = await client.GetAllCartItemsAsync(new GetAllCartItemsRequest());
            ICollection<CartItem> cartItems = new List<CartItem>();
            foreach (var item in reply.Items)
            {
                cartItems.Add(new CartItem()
                {
                    Id = item.Id,
                    CustomerId = item.CustomerId,
                    ItemId = item.ItemId,
                    Quantity = item.Quantity
                });
            }
            return cartItems;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return null;
    }
}