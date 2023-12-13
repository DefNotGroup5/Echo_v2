using System.Collections;
using Application.Shopping.LogicInterfaces;
using Domain.Shopping.DTOs;
using Domain.Shopping.Models;
using GrpcClientServices.Services;

namespace Application.Shopping.Logic;

public class ShoppingCartLogic : IShoppingCartLogic
{
    private readonly ShoppingCartService _shoppingCartService;
    private readonly UsersService _usersService;
    private readonly ItemService _itemService;

    public ShoppingCartLogic(ShoppingCartService shoppingCartService, UsersService usersService, ItemService itemService)
    {
        _shoppingCartService = shoppingCartService;
        _usersService = usersService;
        _itemService = itemService;
    }

    public async Task<CartItem?> AddAsync(CartItemCreationDto dto)
    {
        string? validation = await ValidateCreationDto(dto);
        if (!string.IsNullOrEmpty(validation))
        {
            throw new Exception(validation);
        }
        return await _shoppingCartService.AddToShoppingCartAsync(dto);
    }

    public async Task<ICollection<CartItem>?> GetAllAsync()
    {
        return await _shoppingCartService.GetAllCartItemsAsync();
    }

    public async Task<ICollection<CartItem>?> GetAllByCustomerId(int id)
    {
        ICollection<CartItem>? items = await _shoppingCartService.GetAllCartItemsAsync();
        ICollection<CartItem> userSpecificItems = new List<CartItem>();
        if (items != null)
            foreach (var item in items)
            {
                if (item.CustomerId == id)
                {
                    userSpecificItems.Add(item);
                }
            }

        return userSpecificItems;
    }

    public async Task ClearCart(int customerId)
    {
        try
        {
            await _shoppingCartService.ClearCart(customerId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<string?> ValidateCreationDto(CartItemCreationDto dto)
    {
        User? user = await _usersService.GetByIdAsync(dto.CustomerId);
        if (user == null)
        {
            return "User does not exist!";
        }
        Item? item = await _itemService.GetItemByIdAsync(dto.ItemId);
        if (item == null)
        {
            return "Item does not exist!";
        }
        ICollection<CartItem>? items = await GetAllByCustomerId(user.Id);
        if (items != null)
        {
            foreach (var cartItem in items )
            {
                if (cartItem.ItemId == dto.ItemId)
                    return "Sorry, for now you are only able to add once to the shopping cart! Adjust quantity there";
            }
        }
        if(dto.Quantity > 10 ||dto.Quantity < 1)
        {
            return "Insert proper quantity!";
        }
        return "";         
    }
}